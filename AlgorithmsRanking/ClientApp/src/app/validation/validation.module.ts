import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { SharedModule } from '../shared/shared.module';

import { ControlMessagesComponent } from './control-messages.component';

@NgModule({
    imports: [
        FormsModule,
        SharedModule
    ],
    declarations: [
        ControlMessagesComponent
    ],
    providers: [

    ],
    exports: [
        ControlMessagesComponent,
        FormsModule
    ],
})
export class ValidationModule { }
