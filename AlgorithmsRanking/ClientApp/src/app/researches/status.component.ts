import { Component, OnInit, Input } from '@angular/core';

import { ResearchStatus } from './researches.models';

@Component({
    selector: 'research-status',
    template: `
        <i [attr.class]="'fa fa-' + value.icon" [style.color]="value.color" [title]="value.name"></i>
        <span *ngIf="showLabel">{{ value.name }}</span>
    `
})
export class ResearchStatusComponent implements OnInit {
    @Input() code: number;
    @Input() showLabel: boolean = false;

    value: ResearchStatus;

    constructor() { }

    ngOnInit() {
        this.value = ResearchStatus.byCode(this.code) || ResearchStatus.closed;
    }
}
