import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { AlgorithmsService } from './algorithms.service';
import { Algorithm } from './algorithms.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Алгоритм '{{ value.name }}'</h3>
            <algorithm-form
                [value]="value"
                (save)="submit($event)"
                (cancel)="cancel()">
            </algorithm-form>
        </div>
    `
})
export class AlgorithmsEditComponent implements OnInit {
    value: Algorithm;

    constructor(
        private _algorithms: AlgorithmsService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => this._algorithms.getAlgorithm(+params['id']))
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }

    public submit(value: Algorithm) {
        if (!value) { return; }

        this.value = value;
        this._algorithms.putAlgorithm(this.value.id, this.value)
            .subscribe(
                result => this._algorithms.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._algorithms.gotoList();
    }
}
