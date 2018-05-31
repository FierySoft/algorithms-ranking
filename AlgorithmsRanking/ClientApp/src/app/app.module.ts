import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';

import { AppRoutingModule, appComponents } from './app.routing';
import { AppComponent } from './app.component';

import { SharedModule } from './shared/shared.module';
import { AlgorithmsModule } from './algorithms/algorithms.module';

@NgModule({
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        SharedModule,
        AlgorithmsModule,
        AppRoutingModule
    ],
    declarations: [
        ...appComponents
    ],
    providers: [

    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {

}
