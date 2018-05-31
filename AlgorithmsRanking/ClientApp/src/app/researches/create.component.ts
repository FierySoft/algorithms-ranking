import { Component } from '@angular/core';

import { ResearchesService } from './researches.service';
import { ResearchUpdate } from './researches.models';

@Component({
    template: `
        <h3>Новое исследование</h3>
        <research-form
            [value]="value"
            (save)="submit($event)"
            (cancel)="cancel()">
        </research-form>
    `
})
export class ResearchesCreateComponent {
    value: ResearchUpdate;

    constructor(private _researches: ResearchesService) {
        this.value = new ResearchUpdate();
    }

    public submit(value: ResearchUpdate) {
        if (!value) { return; }

        this.value = value;
        this._researches.postResearch(this.value)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
