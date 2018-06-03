import { Component, OnInit, Input } from '@angular/core';

import { Comment } from '../researches.models';
import { CommentsService } from './comments.service';

@Component({
    selector: 'comments-list',
    template: `
        <div *ngIf="items">
            <div *ngFor="let c of items" class="panel panel-default">
                <div class="panel-heading">
                    <span><b>{{ c.author }}</b> at {{ c.postedAt | date:'yyyy-MM-dd HH:mm:ss' }}</span>
                    <span class="pull-right" style="cursor:pointer"><a>#{{ c.id }}</a></span>
                </div>
                <div class="panel-body">
                    {{ c.content }}
                </div>
            </div>
        </div>
    `
})
export class CommentsListComponent implements OnInit {
    @Input() items: Comment[] = [];

    constructor() { }

    ngOnInit() { }
}
