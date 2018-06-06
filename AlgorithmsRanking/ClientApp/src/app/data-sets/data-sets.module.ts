import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ValidationModule } from '../validation/validation.module';
import { AccountModule } from '../account/account.module';

import { DataSetsRoutingModule, dataSetsComponents } from './data-sets.routing';
import { DataSetsService } from './data-sets.service';

@NgModule({
    imports: [
        SharedModule,
        ValidationModule,
        AccountModule,
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
