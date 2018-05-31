import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';

import { DataSetsRoutingModule, dataSetsComponents } from './data-sets.routing';
import { DataSetsService } from './data-sets.service';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        DataSetsRoutingModule
    ],
    declarations: [
        ...dataSetsComponents
    ],
    providers: [
        DataSetsService
    ],
    exports: [

    ]
})
export class DataSetsModule {

}
