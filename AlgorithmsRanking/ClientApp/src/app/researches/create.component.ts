import { Component, OnInit } from '@angular/core';

import { ResearchesService } from './researches.service';
import { ResearchForm, ResearchInitForm } from './researches.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Новое исследование</h3>
            <research-form
                [value]="value.init"
                [algorithms]="value.algorithms"
                [dataSets]="value.dataSets"
                [executors]="value.executors"
                (save)="submit($event)"
                (cancel)="cancel()">
            </research-form>
        </div>
    `
})
export class ResearchesCreateComponent implements OnInit {
    value: ResearchForm;

    constructor(private _researches: ResearchesService) { }

    ngOnInit() {
        this._researches.getResearchCreate()
            .subscribe(
                result => { this.value = result; this.value.init = new ResearchInitForm(); },
                error => console.log(error)
            );
    }

    public submit(value: ResearchInitForm) {
        if (!value) { return; }

        this.value.init = value;
        this._researches.postResearch(this.value.init)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
