import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule, appComponents } from './app.routing';
import { AppComponent } from './app.component';


@NgModule({
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
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
