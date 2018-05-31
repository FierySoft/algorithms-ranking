import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AceEditorModule } from 'ng2-ace-editor';

import { SpinnerComponent } from './spinner.component';
import { ErrorComponent } from './error.component';
import { PersonNameValidatorDirective } from './person-name.validator';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        AceEditorModule
    ],
    declarations: [
        SpinnerComponent,
        ErrorComponent,
        PersonNameValidatorDirective
    ],
    providers: [

    ],
    exports: [
        CommonModule,
        SpinnerComponent,
        ErrorComponent,
        PersonNameValidatorDirective
    ],
    entryComponents: [

    ]
})
export class SharedModule {

}
