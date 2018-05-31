import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AlgorithmsComponent } from './algorithms.component';
import { AlgorithmsListComponent } from './list.component';
import { AlgorithmFormComponent } from './form.component';
import { AlgorithmsCreateComponent } from './create.component';
import { AlgorithmsEditComponent } from './edit.component';


const routes: Routes = [
    {
        path: 'algorithms',
        component: AlgorithmsComponent,
        children: [
            { path: '', component: AlgorithmsListComponent },
            { path: 'create', component: AlgorithmsCreateComponent },
            { path: ':id', component: AlgorithmsEditComponent }
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
export class AlgorithmsRoutingModule {

}


export const algorithmsComponents = [
    AlgorithmsComponent,
    AlgorithmFormComponent,
    AlgorithmsListComponent,
    AlgorithmsCreateComponent,
    AlgorithmsEditComponent
];
