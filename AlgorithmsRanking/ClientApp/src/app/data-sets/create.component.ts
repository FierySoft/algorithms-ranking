import { Component } from '@angular/core';

import { DataSetsService } from './data-sets.service';
import { DataSet } from './data-sets.models';

@Component({
    template: `
        <div class="form-horizontal">
            <h3>Новый набор данных</h3>
            <div class="col-md-6">
                <data-set-form
                    [value]="value"
                    (save)="submit($event)"
                    (cancel)="cancel()">
                </data-set-form>
            </div>
            <div class="col-md-offset-1 col-md-5">
                <label class="control-label">Файлы</label>
                <div>
                    <file-upload
                        #files
                        accept=".jpg,.png,.pdf,.txt"
                        type="primary-inverse"
                        [multiple]="true"
                        [preview]="false"
                        (upload)="setAttachments($event)">
                    </file-upload>
                </div>
            </div>
        </div>
    `
})
export class DataSetsCreateComponent {
    value: DataSet;

    constructor(private _dataSets: DataSetsService) {
        this.value = new DataSet();
    }

    setAttachments(urls: string[]) {
        if (!urls) { return; }
        this.value.files = urls;
        this.value.filesCount = urls.length;
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
