import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AceEditorModule } from 'ng2-ace-editor';

import { SpinnerComponent } from './spinner.component';
import { ErrorComponent } from './error.component';
import { FileUploadComponent } from './file-upload.component';
import { FileSizePipe } from './file-size.pipe';

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
        FileSizePipe
    ],
    providers: [

    ],
    exports: [
        CommonModule,
        SpinnerComponent,
        ErrorComponent,
        FileUploadComponent,
        FileSizePipe
    ],
    entryComponents: [

    ]
})
export class SharedModule {

}
