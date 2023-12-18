import React from 'react';
import ImgNoImg from '../noImg.jpg';
import './Images.css'

const ImagesComponent = ({ images }) => {

    const renderImage = (img, index) => {
        if (!img || img.length === 0) {
            return <img className="bookSmallImg" key={index} alt="Img" src={ImgNoImg} />;
        }
        return <img className="bookSmallImg" key={index} alt="Img" src={`data:image/jpeg;base64,${img}`} />;
    }

    return (
        <div className="Images">
            <img className="selectedImg" alt="Img" src={`data:image/jpeg;base64,${images[0]}`} />
            <div className="smallImages">
                {images.map((image, index) => (
                    renderImage(image, index++)
                ))}
            </div>
        </div>
    );
};

export default ImagesComponent;




