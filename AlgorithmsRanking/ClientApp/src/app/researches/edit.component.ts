import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { ResearchesService } from './researches.service';
import { ResearchForm, ResearchInitForm } from './researches.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Исследование '{{ value.init.name }}'</h3>
            <research-form
                [value]="value.init"
                [algorithms]="value.algorithms"
                [dataSets]="value.dataSets"
                (save)="submit($event)"
                (cancel)="cancel()">
            </research-form>
            <br><br>
            <comments [researchId]="value.id"></comments>
        </div>
    `
})
export class ResearchesEditComponent implements OnInit {
    value: ResearchForm;

    constructor(
        private _researches: ResearchesService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => {
                return this._researches.getResearchEdit(+params['id']);
            })
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }

    public submit(value: ResearchInitForm) {
        if (!value) { return; }

        this.value.init = value;
        this._researches.putResearch(this.value.id, this.value.init)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
