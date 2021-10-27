<template>
  <v-app id="vue-app">
    <v-main>
      <v-container style="max-width: 600px">
        <v-text-field v-model="messages.$params.search" :loading="contentLoading">
          <template v-slot:append style="margin: auto" v-if="activeChips.length > 0">
            <div style="margin: auto">
              <v-chip close v-for="term in activeChips" @click:close="chipClosed(term)">{{ term }}</v-chip>
            </div>
          </template>
        </v-text-field>

        <div style="display: flex; justify-content: center">
          <v-chip style="margin: 5px 5px" v-for="searchTerm in unselectedChips" @click="addChip(searchTerm)">{{ searchTerm }}</v-chip>
        </div>
      </v-container>
      <v-container style="display: flex; flex-wrap: wrap; justify-content: center">
        <div style="width: 100%; display: flex; justify-content: center" v-if="messages.$items.length > 0">
          <div style="width: 100%; display: flex; justify-content: center; max-width: 600px">
            <v-pagination style="max-width: 60%" v-model="messages.$page" class="my-4" :length="messages.$pageCount" />
          </div>
        </div>
        <div v-for="message in messages.$items" style="margin: 10px 10px" v-if="messages.$items.length > 0">
          <MessageCard
              v-if="!contentLoading"
              :id="message.originalId" :text="message.text" :date="getDateFormat(message.createdAt)"
              :likes="message.favorites" :shares="message.shares" :user-name="message.screenName"
              :profile-picture-link="getProfilePicture(message.screenName)"
              style="min-height: 0; overflow: hidden; margin: 10px 10px"
          />
          <v-skeleton-loader v-else style="overflow: hidden; margin: 10px 10px; min-width: 200px" type="table-heading, article, list-item-avatar" />
        </div>
        <div v-if="messages.$items.length === 0 && !messages.$load.isLoading">
          <h1 class="text-center">No items found</h1>
          <span class="fa-stack fa-2x text-center" style="width: 100%">
            <i class="fas fa-search fa-stack-2x"></i>
            <i class="fas fa-slash fa-flip-vertical fa-stack-2x"></i>
          </span>
        </div>

        <div style="width: 100%; display: flex; justify-content: center" v-if="messages.$items.length > 0">
          <div style="width: 100%; display: flex; justify-content: center; max-width: 600px">
            <v-pagination style="max-width: 60%" v-model="messages.$page" class="my-4" :length="messages.$pageCount" />
          </div>
        </div>
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import * as moment from 'moment';
import Vue from "vue";
import {Component, Watch} from "vue-property-decorator";
import MessageCard from "@/components/MessageCard.vue";
import {MessageListViewModel, UserListViewModel} from "@/viewmodels.g";
import {Message} from "@/models.g";
import MessageDefaultDataSource = Message.DataSources.MessageDefaultDataSource;

@Component({
  components: {MessageCard},
})
export default class App extends Vue {
  activeChips: string[] = [];
  chipSearchTerms: string[] = ['TESLA', 'Elon', 'NHTSA'];
  dataSource = new MessageDefaultDataSource();

  messages: MessageListViewModel = new MessageListViewModel();
  users: UserListViewModel = new UserListViewModel();

  get unselectedChips(): string[] {
    return this.chipSearchTerms.filter(x => this.activeChips.indexOf(x.toLocaleUpperCase().trim()) === -1);
  }

  chipClosed(value: string) {
    let index = this.activeChips.indexOf(value.toLocaleUpperCase().trim());
    if (index < 0) return;

    this.activeChips.splice(index, 1);
  }

  addChip(value: string) {
    this.activeChips.push(value.toLocaleUpperCase().trim());
  }

  @Watch('messages.$page')
  @Watch('messages.$params.search')
  @Watch('activeChipsString')
  pageChanged() {
    window.scrollTo({ top: 0, behavior: 'smooth'});
    this.dataSource.activeChips = this.activeChipsString;
    this.messages.$load().then(_ => {
      let arr: string[] = this.messages.$items.filter(x => x?.screenName !== null)
        .map(x => x.screenName)
        .filter((value, index, array) => array.indexOf(value) === index) as string[];
      this.users.getUsersOnPage(arr);
    });
  }

  get contentLoading(): boolean {
    return this.users.getUsersOnPage.isLoading || this.messages.$load.isLoading;
  }

  get activeChipsString(): string {
    return this.activeChips.join('*');
  }

  getProfilePicture(screenName: string): string {
    if (!this.users.getUsersOnPage.hasResult || !this.users.getUsersOnPage?.result) return '';
    return this.users.getUsersOnPage.result.find(x => x.screenName === screenName)?.profilePictureLink ?? '';
  }

  getDateFormat(date: Date): string {
    return (moment(date)).format('lll') + ' ' + date.toLocaleDateString('en', {timeZoneName: 'short'}).split(' ').pop();
  }

  get getUsersOnPage() {
    return this.users.getUsersOnPage;
  }

  mounted() {
    this.users.getUsersOnPage.setConcurrency('cancel');

    this.dataSource.activeChips = this.activeChipsString;
    this.messages.$dataSource = this.dataSource;
    this.messages.$load.setConcurrency('cancel');
    this.messages.$pageSize = 10;

    this.pageChanged();
  }
}
</script>

<style lang="scss">
.v-input__append-inner {
  margin: auto !important;
}
</style>
