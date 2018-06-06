import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard, AuthGroups } from '../account';

import { DataSetsComponent } from './data-sets.component';
import { DataSetsListComponent } from './list.component';
import { DataSetFormComponent } from './form.component';
import { DataSetsCreateComponent } from './create.component';
import { DataSetsEditComponent } from './edit.component';


const routes: Routes = [
    {
        path: 'data-sets',
        component: DataSetsComponent,
        children: [
            { path: '', component: DataSetsListComponent },
            { path: 'create', canActivate: [ AuthGuard ], data: { auth: AuthGroups.fullAccess }, component: DataSetsCreateComponent },
            { path: ':id', canActivate: [ AuthGuard ], data: { auth: AuthGroups.fullAccess }, component: DataSetsEditComponent }
        ]
    },
];


@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ],
})
export class DataSetsRoutingModule {

}


export const dataSetsComponents = [
    DataSetsComponent,
    DataSetFormComponent,
    DataSetsListComponent,
    DataSetsCreateComponent,
    DataSetsEditComponent
];
