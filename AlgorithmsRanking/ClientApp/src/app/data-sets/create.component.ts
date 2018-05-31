import { Component } from '@angular/core';

import { DataSetsService } from './data-sets.service';
import { DataSet } from './data-sets.models';

@Component({
    template: `
        <h3>Новый набор данных</h3>
        <data-set-form
            [value]="value"
            (save)="submit($event)"
            (cancel)="cancel()">
        </data-set-form>
    `
})
export class DataSetsCreateComponent {
    value: DataSet;

    constructor(private _dataSets: DataSetsService) {
        this.value = new DataSet();
    }

    public submit(value: DataSet) {
        if (!value) { return; }

        this.value = value;
        this._dataSets.postDataSet(this.value)
            .subscribe(
                result => this._dataSets.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._dataSets.gotoList();
    }
}
