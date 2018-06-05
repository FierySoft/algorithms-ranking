import { Component, Input } from '@angular/core';

import { Comment } from '../researches.models';
import { AccountService } from '../../account/account.service';

@Component({
    selector: 'comments-list',
    template: `
        <div *ngIf="items">
            <div *ngFor="let c of items">
                <div *ngIf="isMe(c.author)" class="container is-author">
                    <p>{{ c.content }}</p>
                    <span class="time-left">{{ c.postedAt | date:'yyyy-MM-dd HH:mm' }}</span>
                </div>
                <div *ngIf="!isMe(c.author)" class="container not-author">
                    <p>{{ c.content }}</p>
                    <span class="time-right">{{ c.postedAt | date:'yyyy-MM-dd HH:mm' }}</span>
                </div>
            </div>
        </div>
        <h5 *ngIf="!items?.length" class="text-center"><i>Сообщений пока нет</i></h5>
    `,
    styles: [`
        .container {
            width: 100%;
            border: 1px solid #dedede;
            border-radius: 15px;
            padding: 10px;
            margin: 5px 0;
        }
        .is-author {
            margin-left: 40%;
            width: 60%;
            border-color: #39a80e;
            background-color: #e2ffc7;
            text-align: right;
        }
        .not-author {
            width: 60%;
            margin: 10px 0;
            background-color: #f1f1f1;
        }
        .container::after {
            content: "";
            clear: both;
            display: table;
        }
        .container img {
            float: left;
            max-width: 60px;
            width: 100%;
            margin-right: 20px;
            border-radius: 50%;
        }
        .container img.right {
            float: right;
            margin-left: 20px;
            margin-right:0;
        }
        .time-right {
            float: right;
            color: #aaa;
        }
        .time-left {
            float: left;
            color: #999;
        }
    `]
})
export class CommentsListComponent {
    @Input() items: Comment[] = [];

    constructor(private _account: AccountService) { }

    isMe(author: string): boolean {
        return author === this._account.retrieveUserInfo().userName;
    }
}
