<template>
  <v-card
      class="mx-auto"
      color="#26c6da"
      dark
      max-width="400"
  >
    <v-card-title>
      <span style="font-size: 12px">{{ id }}</span>
      <v-row align="center" justify="end"><span class="font-weight-light" style="font-size: 15px">{{ date }}</span></v-row>
    </v-card-title>

    <v-card-text class="text-h5 font-weight-bold">
      <!-- TODO: @\w+ RegExr to pull out @'s -->
      {{ text }}
    </v-card-text>

    <v-card-actions>
      <v-list-item class="grow">
        <v-list-item-avatar color="grey darken-3">
          <v-img
              class="elevation-6"
              alt=""
              :src="profilePictureLink"
          ></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>{{ userName }}</v-list-item-title>
        </v-list-item-content>

        <v-icon class="mr-1">
          fas fa-heart
        </v-icon>
        <span class="subheading mr-2">{{ likes }}</span>
        <span class="mr-1">·</span>
        <v-icon class="mr-1">
          fas fa-retweet
        </v-icon>
        <span class="subheading">{{ shares }}</span>
      </v-list-item>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import {Component, Prop, Vue} from 'vue-property-decorator';

@Component({})
export default class MessageCard extends Vue {
  @Prop({ type: String, required: true })
  id!: string;

  @Prop({ type: String, required: true })
  text!: string;

  @Prop({ type: String, required: true })
  date!: string;

  @Prop({ type: Number, required: true })
  likes!: number;

  @Prop({ type: Number, required: true })
  shares!: number;

  @Prop({ type: String, required: true })
  userName!: string;

  @Prop({ type: String, required: true })
  profilePictureLink!: string;
}
</script>

<style lang="scss" scoped>

</style>
