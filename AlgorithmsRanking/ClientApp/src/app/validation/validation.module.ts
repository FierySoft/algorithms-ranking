import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ControlMessagesComponent } from './control-messages.component';
import { PersonNameValidatorDirective } from './person-name.validator';

@NgModule({
    imports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        ControlMessagesComponent,
        PersonNameValidatorDirective
    ],
    providers: [

    ],
    exports: [
        ControlMessagesComponent,
        PersonNameValidatorDirective,
        FormsModule
    ],
})
export class ValidationModule { }
