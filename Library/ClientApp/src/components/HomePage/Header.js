import React, { Component } from 'react';
import './Header.css'
import HeaderFon from './Img/HeaderFon.gif';

export class Scroll extends Component {
    static displayName = Scroll.name;

    render() {

        return (
            <div className="headerComponent">
                <img className="header" alt="Header" src={HeaderFon} />
            </div>
        );
    };
}