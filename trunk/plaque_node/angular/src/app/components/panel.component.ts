import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'panel',
    template: '<h1>{{text}}</h1>'
})
export class PanelComponent implements OnInit {

    text: string;

    constructor() {
    }

    ngOnInit(): void {
        this.text  = "Panel";
    }
};