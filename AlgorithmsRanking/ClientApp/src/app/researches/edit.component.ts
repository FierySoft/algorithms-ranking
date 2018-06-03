import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { ResearchesService } from './researches.service';
import { ResearchUpdate, ResearchForm } from './researches.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Исследование '{{ value.model.name }}'</h3>
            <research-form
                [value]="value"
                (save)="submit($event)"
                (cancel)="cancel()">
            </research-form>
            <br><br>
            <comments [researchId]="id"></comments>
        </div>
    `
})
export class ResearchesEditComponent implements OnInit {
    value: ResearchForm;
    id: number;

    constructor(
        private _researches: ResearchesService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => {
                this.id = +params['id'];
                return this._researches.getResearchEdit(this.id);
            })
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }

    public submit(value: ResearchUpdate) {
        if (!value) { return; }

        this.value.model = value;
        this._researches.putResearch(this.id, this.value.model)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
