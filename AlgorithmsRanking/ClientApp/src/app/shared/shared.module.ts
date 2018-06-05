import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AceEditorModule } from 'ng2-ace-editor';

import { SpinnerComponent } from './spinner.component';
import { ErrorComponent } from './error.component';
import { FileUploadComponent } from './file-upload.component';
import { FileSizePipe } from './file-size.pipe';
import { Popover, PopoverContent } from './popover.component';
import { PersonCardComponent } from './person-card.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        AceEditorModule
    ],
    declarations: [
        SpinnerComponent,
        ErrorComponent,
        FileUploadComponent,
        FileSizePipe,
        Popover, PopoverContent,
        PersonCardComponent
    ],
    providers: [

    ],
    exports: [
        CommonModule,
        SpinnerComponent,
        ErrorComponent,
        FileUploadComponent,
        FileSizePipe,
        Popover, PopoverContent,
        PersonCardComponent
    ],
    entryComponents: [

    ]
})
export class SharedModule {

}
