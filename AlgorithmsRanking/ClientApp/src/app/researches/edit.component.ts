import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { ResearchesService } from './researches.service';
import { ResearchUpdate } from './researches.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Исследование '{{ value.name }}'</h3>
            <research-form
                [value]="value"
                (save)="submit($event)"
                (cancel)="cancel()">
            </research-form>
        </div>
    `
})
export class ResearchesEditComponent implements OnInit {
    value: ResearchUpdate;

    constructor(
        private _researches: ResearchesService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => this._researches.getResearch(+params['id']))
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }

    public submit(value: ResearchUpdate) {
        if (!value) { return; }

        this.value = value;
        this._researches.putResearch(this.value.id, this.value)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
