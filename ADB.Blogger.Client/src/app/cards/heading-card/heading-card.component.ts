import { Component, Input } from '@angular/core';
import { Post } from 'src/app/models/post.model';

@Component({
  selector: 'app-heading-card',
  templateUrl: './heading-card.component.html',
  styleUrls: ['./heading-card.component.css']
})
export class HeadingCardComponent {
  emptyAuthor = {
    firstName: '--',
    surname: '--',
  }

  emptyPost = {
    id: '',
    body:'',
    createdBy: this.emptyAuthor,
    actions: [],
    title : 'No title available',
    createdAt : new Date()
  }



  @Input() post: Post  = this.emptyPost;
}
