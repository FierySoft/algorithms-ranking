import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AceEditorModule } from 'ng2-ace-editor';

import { SpinnerComponent } from './spinner.component';
import { ErrorComponent } from './error.component';
import { FileUploadComponent } from './file-upload.component';
import { PersonCardComponent } from './person-card.component';
import { FileSizePipe } from './file-size.pipe';
import { Popover, PopoverContent } from './popover.component';
import { SortableTableDirective, SortableColumnComponent, SortService } from './sortable-table.directive';

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
        PersonCardComponent,
        FileSizePipe,
        Popover, PopoverContent,
        SortableTableDirective, SortableColumnComponent
    ],
    providers: [
        SortService
    ],
    exports: [
        CommonModule,
        SpinnerComponent,
        ErrorComponent,
        FileUploadComponent,
        PersonCardComponent,
        FileSizePipe,
        Popover, PopoverContent,
        SortableTableDirective, SortableColumnComponent
    ],
    entryComponents: [

    ]
})
export class SharedModule {

}
