import { Component, TemplateRef } from '@angular/core';
import {
  CategoriesClient, CreateCategoryCommand, CategoryDto, UpdateCategoryCommand,
  CategoriesVm
} from '../personalmanager-api';
import { faPlus, faEllipsisH } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'categories-component',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoryComponent {

  debug = false;

  vm: CategoriesVm;

  selectedCategory: CategoryDto;

  newCategoryEditor: any = {};
  cagegoryOptionsEditor: any = {};

  newCategoryModalRef: BsModalRef;
  categoryOptionsModalRef: BsModalRef;
  deleteCategoryModalRef: BsModalRef;

  faPlus = faPlus;
  faEllipsisH = faEllipsisH;

  constructor(private categoriesClient: CategoriesClient, private modalService: BsModalService) {
    categoriesClient.get().subscribe(
      result => {
        this.vm = result;
        if (this.vm.categories.length) {
          this.selectedCategory = this.vm.categories[0];
        }
      },
      error => console.error(error)
    );
  }

  showNewCategoryModal(template: TemplateRef<any>): void {
    this.newCategoryModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById("name").focus(), 250);
  }

  newCategoryCancelled(): void {
    this.newCategoryModalRef.hide();
    this.newCategoryEditor = {};
  }

  addCategory(): void {
    let category = CategoryDto.fromJS({
      id: 0,
      name: this.newCategoryEditor.name,
      parentId: this.newCategoryEditor.parentId,
      iconUrl: this.newCategoryEditor.iconUrl,
    });

    this.categoriesClient.create(<CreateCategoryCommand>category).subscribe(
      result => {
        category.id = result;
        this.vm.categories.push(category);
        this.selectedCategory = category;
        this.newCategoryModalRef.hide();
        this.newCategoryEditor = {};
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.newCategoryEditor.error = errors.Title[0];
        }

        setTimeout(() => document.getElementById("name").focus(), 250);
      }
    );
  }

  showCategoryOptionsModal(template: TemplateRef<any>) {
    this.cagegoryOptionsEditor = {
      id: this.selectedCategory.id,
      name: this.selectedCategory.name,
      parentId: this.selectedCategory.parentId,
      iconUrl: this.selectedCategory.iconUrl
    };

    this.categoryOptionsModalRef = this.modalService.show(template);
  }

  updateCategoryOptions() {
    this.categoriesClient.update(this.selectedCategory.id, UpdateCategoryCommand.fromJS(this.cagegoryOptionsEditor))
      .subscribe(
        () => {
          this.selectedCategory.name = this.cagegoryOptionsEditor.name;
          this.selectedCategory.parentId = this.cagegoryOptionsEditor.parentId;
          this.selectedCategory.iconUrl = this.cagegoryOptionsEditor.iconUrl;
          this.categoryOptionsModalRef.hide();
          this.cagegoryOptionsEditor = {};
        },
        error => console.error(error)
      );
  }

  confirmDeleteCategory(template: TemplateRef<any>) {
    this.categoryOptionsModalRef.hide();
    this.deleteCategoryModalRef = this.modalService.show(template);
  }

  deleteCategoryConfirmed(): void {
    this.categoriesClient.delete(this.selectedCategory.id).subscribe(
      () => {
        this.deleteCategoryModalRef.hide();
        this.vm.categories = this.vm.categories.filter(t => t.id != this.selectedCategory.id)
        this.selectedCategory = this.vm.categories.length ? this.vm.categories[0] : null;
      },
      error => console.error(error)
    );
  }

  isChild(category: CategoryDto) {
    return category.parentId && category.parentId !== 0;
  }
}
