import { Component, Input, Output, EventEmitter } from '@angular/core';

import { ResearchesService } from './researches.service';
import { ResearchForm, ResearchUpdate } from './researches.models';

@Component({
    selector: 'research-form',
    templateUrl: './form.component.html',
    styleUrls: [ '../validation/control-validation.css' ]
})
export class ResearchFormComponent {
    @Input() value: ResearchForm;
    @Output('save') onSubmit: EventEmitter<ResearchUpdate> = new EventEmitter();
    @Output('cancel') onCancel: EventEmitter<boolean> = new EventEmitter();

    constructor() { }

    submit() {
        if (!this.value) { return; }
        this.onSubmit.emit(this.value.model);
    }

    cancel() {
        this.onCancel.emit(true);
    }
}
