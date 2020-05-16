/*
import { Component, OnInit } from '@angular/core';
import { Category } from '../category.model';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories: Category[];

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categories = this.categoryService.getCategories();
  }

  onCategorySelected(category: Category) {
    this.categoryService.categorySelected.emit(category);
  }
}
*/
