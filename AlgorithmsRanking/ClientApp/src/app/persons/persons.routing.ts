import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PersonsComponent } from './persons.component';
import { PersonsListComponent } from './list.component';
import { PersonFormComponent } from './form.component';
import { PersonsCreateComponent } from './create.component';
import { PersonsEditComponent } from './edit.component';


const routes: Routes = [
    {
        path: 'persons',
        component: PersonsComponent,
        children: [
            { path: '', component: PersonsListComponent },
            { path: 'create', component: PersonsCreateComponent },
            { path: ':id', component: PersonsEditComponent }
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
export class PersonsRoutingModule {

}


export const personsComponents = [
    PersonsComponent,
    PersonFormComponent,
    PersonsListComponent,
    PersonsCreateComponent,
    PersonsEditComponent
];
