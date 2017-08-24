import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { IdeaService } from '../services/ideas.service'
import { Idea } from '../models/Idea';

@Component({
  selector: 'app-root',
  providers: [IdeaService],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(
    private _httpService: Http,
    private IdeaService : IdeaService
  ) { }

  private ideas: Idea[] = [];

  ngOnInit() {
    this.IdeaService.getIdeas().subscribe(
      masjids => {
        console.log(masjids);
        this.ideas = masjids.data;
      }
    )
  }
}
