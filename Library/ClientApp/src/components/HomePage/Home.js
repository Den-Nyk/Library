import React, { Component } from 'react';
import { Scroll } from './Header';
import { Content } from './Content';
import './Home.css';

export class Home extends Component {
    static displayName = Scroll.name;

    render() {

        return (
            <div className="HomePage">
                <Scroll />
                <Content />
            </div>
        );
    };
}