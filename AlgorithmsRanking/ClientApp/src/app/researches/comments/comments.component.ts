import { Component, Input, OnInit } from '@angular/core';

import { Comment } from '../researches.models';
import { CommentsService } from '../comments/comments.service';

@Component({
    selector: 'comments',
    template: `
        <fieldset style="padding-bottom:20px">
            <legend>Обсуждение ({{ comments ? comments.length : 0 }})</legend>
            <div class="comments-layout col-md-offset-1 col-md-10">
                <div class="comments-list">
                    <comments-list [items]="comments"></comments-list>
                </div>
                <div style="margin:20px 0">
                    <comment-create (post)="postComment($event)"></comment-create>
                </div>
            </div>
        </fieldset>
    `,
    styles: [`
        .comments-layout {
            height: 50%;
        }
        .comments-list {
            height: 500px;
            overflow: auto;
            padding: 0 5px;
        }
    `]
})
export class CommentsComponent implements OnInit {
    @Input() researchId: number;
    comments: Comment[];

    constructor(private _comments: CommentsService) { }

    ngOnInit() {
        this._comments.getCommentsForResearch(this.researchId)
            .subscribe(
                result => this.comments = result,
                error => console.log(error)
            );
    }

    public postComment(content: string) {
        this._comments.postComment(this.researchId, content)
            .subscribe(
                result => { console.log(result); this.comments.push(result); },
                error => console.log(error)
            );
    }
}
