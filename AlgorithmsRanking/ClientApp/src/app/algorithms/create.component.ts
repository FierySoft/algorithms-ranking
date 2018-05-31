import { Component } from '@angular/core';

import { AlgorithmsService } from './algorithms.service';
import { Algorithm } from './algorithms.models';

@Component({
    template: `
        <h3>Новый алгоритм</h3>
        <algorithm-form
            [value]="value"
            (save)="submit($event)"
            (cancel)="cancel()">
        </algorithm-form>
    `
})
export class AlgorithmsCreateComponent {
    value: Algorithm;

    constructor(private _algorithms: AlgorithmsService) {
        this.value = new Algorithm();
    }

    public submit(value: Algorithm) {
        if (!value) { return; }

        this.value = value;
        this._algorithms.postAlgorithm(this.value)
            .subscribe(
                result => this._algorithms.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._algorithms.gotoList();
    }
}
