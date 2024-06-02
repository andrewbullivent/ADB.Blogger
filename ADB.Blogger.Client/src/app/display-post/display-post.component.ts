import { Component, Input, OnInit } from '@angular/core';
import { BloggerApiService } from '../services/blogger-api.service';
import { Post } from '../models/post.model';
import { Author } from '../models/author.model';
import { StateResult } from '../models/state-result.model';
import { ModalService } from '../services/modals.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-display-post',
  templateUrl: './display-post.component.html',
  styleUrls: ['./display-post.component.css']
})
export class DisplayPostComponent implements OnInit {
  @Input() id = '';

  result: StateResult<Post> = {
    data: {
      id: "",
      title: "",
      body: "",
      createdAt: new Date(),
      createdBy: {} as Author,
      actions:[]
    },
    actions:[]
  };

  constructor(private router: Router, private bloggerApiService: BloggerApiService, public modalService: ModalService) {
    
  }

  ngOnInit(): void {
    if (this.id) {
      this.bloggerApiService.getPost(this.id)
        .subscribe(data => {
          if (data) {
            this.result = data;
            console.log(data)
        }
      });
    }
  }

  delete(id:string): void {
    this.bloggerApiService.deletePost(id)
    .subscribe(data => {
      if (data) {
        console.log( data);

      }
      this.modalService.close();
      this.router.navigate(['/post/']);
    });
  }


}
