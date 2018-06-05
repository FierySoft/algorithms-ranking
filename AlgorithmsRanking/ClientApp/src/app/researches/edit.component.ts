import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { ResearchesService } from './researches.service';
import { ResearchForm, ResearchInitForm, ResearchCalculatedForm, ResearchStatus } from './researches.models';

@Component({
    templateUrl: 'edit.component.html',
    styleUrls: [ '../validation/control-validation.css' ]
})
export class ResearchesEditComponent implements OnInit {
    value: ResearchForm;

    constructor(
        private _researches: ResearchesService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => {
                return this._researches.getResearchEdit(+params['id']);
            })
            .subscribe(
                result => {
                    this.value = result;
                    if (!this.value.calculated && this.value.init.executorId) {
                        this.value.calculated = new ResearchCalculatedForm();
                    }
                },
                error => console.log(error)
            );
    }

    public submit(value: ResearchInitForm) {
        if (!value) { return; }

        this.value.init = value;
        this._researches.putResearch(this.value.id, this.value.init)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }


    public get canStart(): boolean {
        return !!this.value.permissions.statusChangeOptions.find(x => x === ResearchStatus.inProgress.code);
    }

    public get canExecute(): boolean {
        return !!this.value.permissions.statusChangeOptions.find(x => x === ResearchStatus.executed.code);
    }

    public get canDecline(): boolean {
        return !!this.value.permissions.statusChangeOptions.find(x => x === ResearchStatus.declined.code);
    }

    public get canClose(): boolean {
        return !!this.value.permissions.statusChangeOptions.find(x => x === ResearchStatus.closed.code);
    }


    public start() {
        if (!this.value) { return; }
        this._researches.startResearch(this.value.id)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public execute() {
        if (!this.value.calculated) { return; }
        this._researches.executeResearch(this.value.id, this.value.calculated)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public decline() {
        if (!this.value) { return; }
        this._researches.declineResearch(this.value.id)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public close() {
        if (!this.value) { return; }
        this._researches.closeResearch(this.value.id)
            .subscribe(
                result => this._researches.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._researches.gotoList();
    }
}
