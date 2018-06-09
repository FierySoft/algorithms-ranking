import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR  } from '@angular/forms';

import { ResearchRate } from './researches.models';

@Component({
    selector: 'array-control',
    template: `
        <ul *ngIf="items" class="list-group" style="margin-bottom:5px">
            <li *ngFor="let item of items; let i = index;" class="list-group-item">
                {{ item.value }}
                <i (click)="removeItem(i)" title="Удалить" class="fa fa-close pull-right" style="cursor:pointer"></i>
            </li>
        </ul>
        <div class="input-group">
            <input type="number"
                [(ngModel)]="addedValue"
                required
                [pattern]="pattern"
                [placeholder]="placeholder"
                class="form-control"
                #added="ngModel">
            <span class="input-group-btn">
                <button
                    (click)="addItem()"
                    [disabled]="added.invalid"
                    class="btn btn-default"
                    type="button">
                    Добавить
                </button>
            </span>
        </div>
    `,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => ArrayControlComponent),
            multi: true
        }
    ]
})
export class ArrayControlComponent implements OnInit, ControlValueAccessor {
    @Input() pattern: string = '';
    @Input() researchId: number;
    @Input() type: number;
    @Input() placeholder: string = 'Введите значение';
    items: ResearchRate[];
    addedValue: number;

    constructor() { }

    ngOnInit() {
        if (!this.items) {
            this.items = [];
        }
    }

    public addItem(): void {
        if (!this.addedValue) { return; }
        this.items.push(new ResearchRate(0, this.researchId, this.type, this.addedValue));
        this.propagateChange(this.items);
        this.addedValue = null;
    }

    public removeItem(index: number): void {
        if (index < 0) { return; }
        if (!confirm(`Удалить элемент?`)) { return; }
        this.items.splice(index, 1);
        this.propagateChange(this.items);
    }


    /* --- ControlValueAccessor -- */
    writeValue = (value: ResearchRate[]) => {
        if (value) {
            this.items = value;
        }
    }
    private propagateChange = (_: any) => { };
    registerOnChange = (fn: any) => this.propagateChange = fn;
    registerOnTouched = (fn: any) => { };
    setDisabledState = (isDisabled: boolean) => { };
}
