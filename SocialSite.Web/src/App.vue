<template>
  <v-app id="vue-app">
    <v-main>
      <v-container style="max-width: 600px">
        <v-text-field v-model="messages.$params.search" :loading="contentLoading"></v-text-field>

        <div style="display: flex; justify-content: center">
          <v-chip style="margin: 5px 5px" close>Test</v-chip>
          <v-chip style="margin: 5px 5px" close>Test</v-chip>
        </div>

        <v-divider></v-divider>

        <div style="display: flex; justify-content: center">
          <v-chip style="margin: 5px 5px" close>Test</v-chip>
          <v-chip style="margin: 5px 5px" close>Test</v-chip>
          <v-chip style="margin: 5px 5px" close>Test</v-chip>
          <v-chip style="margin: 5px 5px" close>Test</v-chip>
        </div>
      </v-container>
      <v-container style="display: flex; flex-wrap: wrap; justify-content: center">
        <div v-for="message in messages.$items" style="margin: 10px 10px" v-if="!contentLoading">
          <MessageCard
                       :id="message.originalId" :text="message.text" :date="getDateFormat(message.createdAt)"
                       :likes="message.favorites" :shares="message.shares" :user-name="message.screenName"
                       :profile-picture-link="getProfilePicture(message.screenName)"
                       style="min-height: 0; overflow: hidden"
          />
        </div>
        <div style="margin: 10px 10px" v-for="cur in getArrayOfSize(30)" v-else>
          <v-skeleton-loader style="width: 400px" type="table-heading, article, list-item-avatar" />
        </div>

        <div style="width: 100%; display: flex; justify-content: center; margin-bottom: 100px">
          <div style="width: 60%; display: flex; justify-content: center">
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

@Component({
  components: {MessageCard},
})
export default class App extends Vue {
  messages: MessageListViewModel = new MessageListViewModel();
  users: UserListViewModel = new UserListViewModel();

  @Watch('messages.$page')
  @Watch('messages.$params.search')
  pageChanged() {
    window.scrollTo({ top: 0, behavior: 'smooth'});
    this.messages.$load().then(_ => {
      let arr: string[] = this.messages.$items.filter(x => x?.screenName !== null).map(x => x.screenName).filter((value, index, array) => array.indexOf(value) === index) as string[];
      this.users.getUsersOnPage(arr);
    });
  }

  get contentLoading(): boolean {
    return this.users.getUsersOnPage.isLoading || this.messages.$load.isLoading;
  }

  getArrayOfSize(len: number): number[] {
    return Array.from(Array(len)).map((v, i) => i);
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

    this.messages.$load.setConcurrency('cancel');
    this.messages.$pageSize = 30;

    this.pageChanged();
  }
}
</script>

<style lang="scss">

</style>
