import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { DataSetsService } from './data-sets.service';
import { DataSet } from './data-sets.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value" class="form-horizontal">
            <h3>Набор данных '{{ value.name }}'</h3>
            <div class="col-md-6">
                <data-set-form
                    [value]="value"
                    (save)="submit($event)"
                    (cancel)="cancel()">
                </data-set-form>
            </div>
            <div class="col-md-offset-1 col-md-5">
                <label class="control-label">Файлы:</label>
                <ul class="list-group">
                    <li *ngFor="let url of value.files; let i = index;" class="list-group-item">
                        <a [href]="url" target="_blank"><b>file_00{{ i + 1 }}</b></a>
                    </li>
                </ul>
                <div *ngIf="!value.files?.length">Файлов нет</div>
            </div>
        </div>
    `
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
