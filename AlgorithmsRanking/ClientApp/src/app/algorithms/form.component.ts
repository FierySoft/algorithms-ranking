import { Component, Input, Output, EventEmitter } from '@angular/core';

import { AlgorithmsService } from './algorithms.service';
import { Algorithm } from './algorithms.models';

@Component({
    selector: 'algorithm-form',
    templateUrl: './form.component.html',
    styleUrls: [ '../validation/control-validation.css' ]
})
export class AlgorithmFormComponent {
    @Input() value: Algorithm;
    @Output('save') onSubmit: EventEmitter<Algorithm> = new EventEmitter();
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
