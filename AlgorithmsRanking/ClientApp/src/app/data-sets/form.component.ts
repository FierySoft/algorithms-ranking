import { Component, Input, Output, EventEmitter } from '@angular/core';

import { DataSetsService } from './data-sets.service';
import { DataSet } from './data-sets.models';

@Component({
    selector: 'data-set-form',
    templateUrl: './form.component.html',
    styleUrls: [ '../validation/control-validation.css' ]
})
export class DataSetFormComponent {
    @Input() value: DataSet;
    @Output('save') onSubmit: EventEmitter<DataSet> = new EventEmitter();
    @Output('cancel') onCancel: EventEmitter<boolean> = new EventEmitter();

    constructor() { }

    submit() {
        if (!this.value) { return; }
        this.onSubmit.emit(this.value);
    }

    cancel() {
        this.onCancel.emit(true);
    }
}
