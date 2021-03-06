import { Component, Input, Output, EventEmitter } from '@angular/core';

import { PersonsService } from './persons.service';
import { Person, Account } from './persons.models';

@Component({
    selector: 'person-form',
    templateUrl: './form.component.html',
    styleUrls: [ '../validation/control-validation.css' ]
})
export class PersonFormComponent {
    @Input() value: Account;
    @Output('save') onSubmit: EventEmitter<Account> = new EventEmitter();
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
