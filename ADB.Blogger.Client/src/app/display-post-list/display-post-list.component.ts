import { Component } from '@angular/core';
import { BloggerApiService } from '../services/blogger-api.service';
import { Post } from '../models/post.model';
import { StateResult } from '../models/state-result.model';

@Component({
  selector: 'app-display-post-list',
  templateUrl: './display-post-list.component.html',
  styleUrls: ['./display-post-list.component.css']
})
export class DisplayPostListComponent {


menuOpen: boolean = false;
viewTile: boolean = true;
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
highlightedPost: Post = this.emptyPost;

result: StateResult<Post[]> = {
  actions: [],
  data: []
};
  

  constructor(private bloggerApiService: BloggerApiService) {

    this.bloggerApiService.getPosts()
      .subscribe(data => {
        if (data && data.data && data.data.length > 0) {
          this.highlightedPost = data.data[0];                 
        }
        this.result = data;
    });
    
  }

  changeView(isTiled: boolean) {
    this.viewTile = isTiled;
    this.menuOpen = false
  }

  open(event:MouseEvent , id: string) {
    event.preventDefault();
    event.stopImmediatePropagation(); 
    console.log(`click event:${id}`);  
  }

}

