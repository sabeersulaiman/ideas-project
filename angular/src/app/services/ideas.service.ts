import { Idea }     from '../models/Idea';
import { Injectable } from '@angular/core';
import { Http }       from '@angular/http';
import { stringify }  from 'querystring';
import { Observable } from 'rxjs/Observable';
import { Config }     from '../models/Config';

import 'rxjs/add/operator/map';

@Injectable()
export class IdeaService {
  private masjids = [];
  private BaseUrl = Config.appBase + 'api/ideas/';
  
  constructor(private http : Http ) {}

  getIdeas() {
    return this.http.get(this.BaseUrl + 'list/0').map(
      (response) => {
        const s = response.json();
        return s;
      }
    );
  }
}