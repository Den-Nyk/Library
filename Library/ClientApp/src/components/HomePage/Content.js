﻿import React, { Component } from 'react';
import './Content.css'
import { TextContent } from './TextContent';
import { Scroll } from '../ScrollOfBooks/Scroll';
import { MoreAboutSite } from './MoreAboutSite'

export class Content extends Component {
    static displayName = Content.name;

    render() {

        return (
            <div className="ContentPage">
                <TextContent />
                <Scroll />
                <MoreAboutSite />
            </div>
        );
    };
}