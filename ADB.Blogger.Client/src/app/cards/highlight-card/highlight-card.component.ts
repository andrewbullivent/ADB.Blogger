import { Component, Input } from '@angular/core';
import { Post } from 'src/app/models/post.model';

@Component({
  selector: 'app-highlight-card',
  templateUrl: './highlight-card.component.html',
  styleUrls: ['./highlight-card.component.css']
})
export class HighlightCardComponent {
  
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



  @Input() post: Post = this.emptyPost;

}
