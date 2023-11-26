import React, { Component } from 'react';
import './TextContent.css'
import ReadImg from './Img/Content.jpg';
import 'bootstrap/dist/css/bootstrap.min.css';

export class TextContent extends Component {
    static displayName = TextContent.name;

    render() {
        return (
            <div className="textImg mb-5 mt-3">
                <div className="content">
                    <img className="img" alt="Img" src={ReadImg} />
                    <div className="text-wrapper">What to Read Now</div>
                    <div className="rectangle" />
                    <ul className="text-on-page">
                        <li>
                            In a vast sea of literature, choosing your next book can be both thrilling and daunting. Whether you're a
                            seasoned reader or just starting, here's a brief guide to help you on your literary journey.
                        </li>
                        <li>
                            Explore New Genres: Try something different—step into genres unexplored. Whether it's historical fiction,
                            magical realism, or speculative fiction, diversify your reading experience.
                        </li>
                        <li>
                            Embrace Contemporary Voices: Dive into the works of contemporary authors to gain fresh perspectives on
                            today's world. Their insights and reflections can be both enlightening and thought-provoking.
                        </li>
                        <li>
                            Rediscover Classics: Revisit timeless classics that have shaped literature. The enduring brilliance of authors
                            like Austen, Dostoevsky, or Wilde offers a timeless reading experience.
                        </li>
                        <li>
                            Follow Recommendations: Stay connected with the literary community. Seek recommendations from trusted sources,
                            be it reviews, podcasts, or online book clubs.
                        </li>
                        <li>
                            Explore Themes That Resonate: Choose books that explore themes relevant to your current interests or
                            challenges. Whether it's self-discovery, resilience, love, or adventure, find stories that speak to you.
                        </li>
                        <li>
                            Award-Winning Picks: Discover acclaimed literature by exploring award-winning books. Prizes like the Booker
                            Prize or Pulitzer Prize often highlight exceptional works.
                        </li>
                        <li>
                            Support Local Authors: Connect with your roots by exploring works from local authors. Supporting local talent
                            fosters a sense of community and offers unique perspectives.
                        </li>
                        <li>
                            Dive into Series: If you crave a more extended literary journey, consider diving into a captivating series or
                            trilogy. The interconnected narratives provide a fulfilling reading experience.
                        </li>
                        <li>
                            Remember, the joy of reading lies in the adventure of discovering new worlds, perspectives, and ideas. Happy
                            reading!
                        </li>
                    </ul>
                </div>
            </div>
        );
    };
}
