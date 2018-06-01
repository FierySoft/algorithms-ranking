import { Component, OnInit } from '@angular/core';

import { ResearchesService } from './researches.service';
import { ResearchUpdate, ResearchForm } from './researches.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Новое исследование</h3>
            <research-form
                [value]="value"
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
                result => { this.value = result; this.value.model = new ResearchUpdate() },
                error => console.log(error)
            );
    }

    public submit(value: ResearchUpdate) {
        if (!value) { return; }

        this.value.model = value;
        this._researches.postResearch(this.value.model)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
