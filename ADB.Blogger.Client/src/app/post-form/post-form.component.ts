import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { BloggerApiService } from '../services/blogger-api.service';
import { NewPostViewModel, PostViewModel } from '../models/post.model';


@Component({
  selector: 'app-post-form',
  templateUrl: './post-form.component.html',
  styleUrls: ['./post-form.component.css']
})
export class PostFormComponent implements OnInit {
  public form: FormGroup;
  id: string = '';
  isAddMode: boolean = false;
  loading: boolean = false;
  submitted: boolean = false

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private blogService: BloggerApiService
  ){
    this.form = new FormGroup({
      title: new FormControl('', Validators.required),
      body: new FormControl('')
    });
  }

  ngOnInit(): void {

    
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;



    if(!this.isAddMode){
      this.blogService.getPost(this.id)
      .pipe(first())
      .subscribe(x=> 
        {
          if(x.actions.length> 1){
            this.form.patchValue(x.data);
          }
          else {
            this.router.navigate(['/post/'+x.data.id]);
          }
        });
    }

  }

  get f() {return this.form.controls;}

  onSubmit(): void {
    this.submitted = true;

    if(this.form.invalid){
      return;
    }

    this.loading = true;
    if(this.isAddMode){
      this.createPost();
    }
    else{
      this.updatePost();
    }
  }


  private createPost(){
    console.log("created post", this.form.controls);
    const post: NewPostViewModel = {
      title:this.f['title'].value,
      body:this.f['body'].value
    };

    this.blogService.savePost(post)
      .subscribe(res  => 
        {
          console.log(res);
          const post = res; 
          this.router.navigate(['/post/'+post.id]);
        }); 
  }

  private updatePost() {
    console.log("updated post", this.form.controls);
    const post: PostViewModel = {
      id:this.id,
      title:this.f['title'].value,
      body:this.f['body'].value
    };

    this.blogService.updatePost(post)
      .subscribe(res  => 
        {
          console.log(res);
          const post = res; 
          this.router.navigate(['/post/'+post.id]);
        }); 
  }


}
