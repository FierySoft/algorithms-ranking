import { Component } from '@angular/core';

import { DataSetsService } from './data-sets.service';
import { DataSet, Attachment } from './data-sets.models';

@Component({
    templateUrl: 'create.component.html'
})
export class DataSetsCreateComponent {
    value: DataSet;
    acceptedFileFormats: string = '.data,.rar,.zip,.iso,.mdd,.mtf';

    constructor(private _dataSets: DataSetsService) {
        this.value = new DataSet();
    }

    setAttachments(files: Attachment[]) {
        if (!files) { return; }
        this.value.files = files;
        this.value.filesCount = files.length || 0;
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
