import { Component, OnInit, Input } from '@angular/core';

import { Research } from './researches.models';
import { ResearchesService } from './researches.service';

@Component({
    selector: 'research-card',
    templateUrl: 'card.component.html'
})
export class ResearchCardComponent implements OnInit {
    @Input() id: number;
    value: Research;

    constructor(private _researches: ResearchesService) { }

    ngOnInit() {
        if (!this.id) { return; }
        this._researches.getResearch(this.id)
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }
}
