using HtmlAgilityPack;
using Library.Data;
using Library.Domains.BookAuthor;

namespace Library.Services
{
    public class FillDbBookByUrlFromYaBook
    {
        private static string GetTitle(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var titleNode = doc.DocumentNode.SelectSingleNode("//title");

            if (titleNode == null)
            {
                throw new InvalidOperationException("Title not found in HTML.");
            }

            return titleNode.InnerText.Trim();
        }

        private static string ExtractBookTitle(string inputData)
        {
            var title = GetTitle(inputData);

            int startIndex = title.IndexOf('«');
            int endIndex = title.IndexOf('»');

            if (startIndex != -1 && endIndex != -1)
            {
                return title.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
            }

            return title;
        }

        private static string ExtractAuthor(string inputData)
        {
            var title = GetTitle(inputData);
            int hyphenIndex = title.IndexOf('–');
            int commaIndex = title.IndexOf(',');

            if (hyphenIndex != -1 && commaIndex != -1 && hyphenIndex < commaIndex)
            {
                return title.Substring(hyphenIndex + 1, commaIndex - hyphenIndex - 1).Trim();
            }

            return string.Empty;
        }

        private static HtmlNode GetNodeForImgParent(string input)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);

            return doc.DocumentNode.SelectSingleNode("//div[@class='preview__gallery']");
        }

        private static List<byte[]> GetListOfImgs(string input)
        {
            var desiredDiv = GetNodeForImgParent(input);
            List<byte[]> imageBytesList = new List<byte[]>();

            if (desiredDiv != null)
            {
                HtmlNodeCollection divNodes = desiredDiv.SelectNodes(".//div[contains(@class, 'slick-slide slick-active')]");
                if (divNodes != null && divNodes.Count > 0)
                {
                    int iterator = 0;
                    foreach (HtmlNode divNode in divNodes)
                    {
                        if (iterator++ == 1)
                            continue;
                        HtmlNode imgElement = divNode.SelectSingleNode(".//img[@src]");
                        if (imgElement != null && imgElement.Attributes["src"] != null)
                        {
                            string imageUrl = imgElement.Attributes["src"].Value;

                            byte[] imageBytes = LoadImage(imageUrl);

                            imageBytesList.Add(imageBytes);
                        }
                    }
                }
            }

            // Return the list of image byte arrays
            return imageBytesList;
        }

        private static byte[] LoadImage(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return client.GetByteArrayAsync(imageUrl).Result;
            }
        }

        private static List<string> GetTypes(string inputData)
        {
            var document = new HtmlDocument();
            document.LoadHtml(inputData);
            var types = new List<string>();

            var divElement = document.DocumentNode.SelectSingleNode("//div[@class='product-options products-options category-options']");

            if (divElement != null)
            {
                var buttons = divElement.SelectNodes(".//button[@class='ui-btn-select option-button']");

                if (buttons != null && buttons.Any())
                {
                    foreach (var button in buttons)
                    {
                        var spanText = button.SelectSingleNode(".//span")?.InnerText.Trim();

                        if (!string.IsNullOrEmpty(spanText))
                        {
                            types.Add(spanText);
                        }
                    }
                }
            }
            return types;
        }

        private static string GetDescription(string inputData)
        {
            var document = new HtmlDocument();
            document.LoadHtml(inputData);

            var divElement = document.DocumentNode.SelectSingleNode("//div[@class='description']");

            if (divElement != null)
            {
                var pElement = divElement.SelectSingleNode(".//p");

                if (pElement != null)
                {
                    return pElement.InnerText.Trim();
                }
            }
            return "";
        }

        private static async Task<Book> GetBookInfo(string responseData)
        {
            var book = new Book();

            var gettinPublishingHouses = GeneratePubshingHouses();

            Random random = new Random();
            book.YearOfCreation = random.Next(1960, 2016);

            book.Name = ExtractBookTitle(responseData);

            var typesList = new List<BookType>();
            var types = GetTypes(responseData);
            foreach (var type in types)
            {
                typesList.Add(new BookType
                {
                    TypeName = type
                });
            }
            book.Types = typesList;

            var imgsList = new List<Image>();
            var imgs = GetListOfImgs(responseData);
            foreach (var img in imgs)
            {
                imgsList.Add(new Image
                {
                    Img = img
                });
            }
            book.Images = imgsList;

            string description = GetDescription(responseData);
            book.Description = description;

            book.PublishingHouses = await gettinPublishingHouses;
            return book;
        }

        private static string GetAuthorPageLink(string inputData)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(inputData);

            HtmlNode targetSection = doc.DocumentNode.SelectSingleNode("//section[@class='main']");

            if (targetSection != null)
            {
                HtmlNode targetAnchor = targetSection.SelectSingleNode(".//a[starts-with(@href, '/ua/author/view/') and contains(@class, 'product-creators')]");

                if (targetAnchor != null)
                {
                    string href = targetAnchor.GetAttributeValue("href", "");
                    return href.Substring("/ua/author/view/".Length);
                }
            }
            return null;
        }


        private static async Task<Author> GetAuthorInfo(string responseData)
        {
            Author author = new Author();
            string authorName = ExtractAuthor(responseData);
            author.Name = authorName;

            var AuthorPageLink = GetAuthorPageLink(responseData);
            if (AuthorPageLink != null)
            {
                string authorUrl = "https://www.yakaboo.ua/ua/author/view/" + AuthorPageLink;
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(authorUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            author.LinkToYaBook = authorUrl;

                            responseData = await response.Content.ReadAsStringAsync();

                            // Load HTML document
                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(responseData);

                            HtmlNode authorInfoDiv = doc.DocumentNode.SelectSingleNode("(//div[@class='author__info'])");

                            if (authorInfoDiv != null)
                            {
                                var authorDetailValueDiv = authorInfoDiv.SelectNodes(".//div[@class='author__detail-value']");
                                var date = authorDetailValueDiv[1];
                                string authorDetailValue = date?.InnerText?.Trim();

                                author.DateOfBirthday = authorDetailValue;
                            }
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            responseData = await response.Content.ReadAsStringAsync();

                            // Load HTML document
                            HtmlDocument doc = new HtmlDocument();
                            doc.LoadHtml(responseData);

                            HtmlNode authorInfoDiv = doc.DocumentNode.SelectSingleNode("(//div[@class='author__info'])");

                            if (authorInfoDiv != null)
                            {
                                var authorDetailValueDiv = authorInfoDiv.SelectSingleNode(".//div[@class='show-more-text']");
                                string authorDetailValue = authorDetailValueDiv?.InnerText?.Trim();

                                author.Description = authorDetailValue;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            return author;
        }

        private enum EPublishingHouses
        {
            A_ba_ba_ga_la_ma_ga,
            Scholastic,
            Puffin,
            Penguin,
            Stary_Lev_Publishing_House,
            Book_club_Family_Leisure_Club,
            HarperCollins_Publishers
        }

        private enum ELanguages
        {
            English,
            Ukraine,
            Germany
        }

        private static async Task<ICollection<PublishingHouse>> GeneratePubshingHouses()
        {
            ICollection<PublishingHouse> publishingHouses = new List<PublishingHouse>();

            Random random = new Random();
            int randomNumber = random.Next(1, 4);

            List<EPublishingHouses> usedValues = new List<EPublishingHouses>();
            for (int i = 0; i < randomNumber; i++)
            {
                EPublishingHouses randomValue;
                do
                {
                    randomValue = GetRandomEnumPublishingHouse<EPublishingHouses>(random);
                } while (usedValues.Contains(randomValue));

                publishingHouses.Add(new PublishingHouse
                {
                    PublishingHouseName = randomValue.ToString()
                });
                usedValues.Add(randomValue);

                int randomNumberForLanguage = random.Next(1, 3);
                for (int iLanguage = 0; iLanguage < randomNumberForLanguage; iLanguage++)
                {
                    List<ELanguages> usedValuesLanguage = new List<ELanguages>();

                    // Generate a random enum value that has not been used before
                    ELanguages randomValueLanguage;
                    do
                    {
                        randomValueLanguage = GetRandomEnumLanguage<ELanguages>(random);
                    } while (usedValuesLanguage.Contains(randomValueLanguage));

                    // Add the used value to the list
                    usedValuesLanguage.Add(randomValueLanguage);
                    if (publishingHouses.ElementAt(i).Languages == null)
                    {
                        publishingHouses.ElementAt(i).Languages = new List<Language>();
                    }
                    publishingHouses.ElementAt(i).Languages.Add(new Language
                    {
                        LanguageName = randomValueLanguage.ToString(),
                        NumberOfPages = random.Next(100, 1500),
                        YearOfPublication = random.Next(2015, 2023),
                        MoreInfo = new MoreInfoAboutBook
                        {
                            Illustrations = random.Next(2) == 1,
                            Weight = random.Next(100, 1000),
                            TypeOfCover = random.Next(2) == 1 ? "Тверда" : "М'яка",
                            Format = $"{random.Next(50, 300)} * {random.Next(50, 300)}"
                        }
                    });
                }
            }

            return publishingHouses;
        }

        private static T GetRandomEnumPublishingHouse<T>(Random random)
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }

        private static T GetRandomEnumLanguage<T>(Random random)
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }

        public static async Task FillDbByYaBookUrl(string url, BookAutorDbContext _context)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        GeneratePubshingHouses();

                        Book Book = await GetBookInfo(responseData);
                        Book.LinkToYaBook = url;

                        Book existingBook = _context.Books.FirstOrDefault(b => b.Name == Book.Name);

                        if (existingBook == null)
                        {
                            Author Author = await GetAuthorInfo(responseData);
                            BookAuthor bookAuthor = new BookAuthor
                            {
                                Author = Author,
                                Book = Book
                            };

                            Author existingAuthor = _context.Authors.FirstOrDefault(a => a.Name == Author.Name);
                            if (existingAuthor != null)
                            {
                                Author = existingAuthor;
                                bookAuthor = new BookAuthor
                                {
                                    Author = Author,
                                    Book = Book
                                };
                            }

                            _context.Books.Add(Book);
                            if (existingAuthor == null)
                            {
                                _context.Authors.Add(Author);
                            }
                            _context.BookAuthors.Add(bookAuthor);

                            _context.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("Such book already exist");
                            throw new Exception("Such book already exist");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }
    }
}
