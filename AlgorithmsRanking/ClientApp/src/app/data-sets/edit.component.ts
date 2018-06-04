import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { DataSetsService } from './data-sets.service';
import { DataSet, Attachment } from './data-sets.models';

@Component({
    templateUrl: 'edit.component.html'
})
export class DataSetsEditComponent implements OnInit {
    value: DataSet;

    constructor(
        private _dataSets: DataSetsService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => this._dataSets.getDataSet(+params['id']))
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }

    public submit(value: DataSet) {
        if (!value) { return; }

        this.value = value;
        this._dataSets.putDataSet(this.value.id, this.value)
            .subscribe(
                result => this._dataSets.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._dataSets.gotoList();
    }
}
