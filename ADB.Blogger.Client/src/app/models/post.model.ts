import { Author } from "./author.model";

export interface Post {
    id: string;
    title: string;
    body: string;
    createdAt: Date;
    createdBy: Author;
    actions: string[];
}

export interface NewPostViewModel{
    title: string;
    body: string;   
}

export interface PostViewModel extends NewPostViewModel{
    id: string|null;    
}