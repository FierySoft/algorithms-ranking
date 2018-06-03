import { Component, Input, Output, EventEmitter } from '@angular/core';

import { ResearchesService } from './researches.service';
import { ResearchInitForm, EntityListItem } from './researches.models';

@Component({
    selector: 'research-form',
    templateUrl: './form.component.html',
    styleUrls: [ '../validation/control-validation.css' ]
})
export class ResearchFormComponent {
    @Input() value: ResearchInitForm;
    @Input() algorithms: EntityListItem[] = [];
    @Input() dataSets: EntityListItem[] = [];
    @Input() executors: EntityListItem[] = [];

    @Output('save') onSubmit: EventEmitter<ResearchInitForm> = new EventEmitter();
    @Output('cancel') onCancel: EventEmitter<boolean> = new EventEmitter();

    constructor() { }

    submit() {
        if (!this.value) { return; }
        if (this.value) { this.onSubmit.emit(this.value); }
    }

    cancel() {
        this.onCancel.emit(true);
    }
}
