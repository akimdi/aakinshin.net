---
layout: post
title: "To Refactor Or Not To Refactor?"
date: "2014-07-19"
lang: en
tags:
- PerfectCode
- Refactoring
redirect_from:
- /en/blog/dev/refactoring/
---

I like refactoring. No, I love refactoring. No, not even like this. I awfully love refactoring.

I hate bad code and bad architecture. I feel quite creepy when I design a new feature and the near-by class contains absolute mess. I just can’t look at the sadly-looking variables. Sometimes before falling asleep I close my eyes and imagine what could be improved in the project. Sometimes I wake up at 3:00AM and go to my computer to improve something. I want to have not just code, but a masterpiece that is pleasant to look at, that is pleasant to work with at any stage of the project.
 
If you just a little bit share my feelings we have something to talk about. The matter is that over some time something inside me began to hint that it’s a bad idea to refactor all code, everywhere and all the time. Understand me correctly – code should be good (even better when it’s ideal), but in real life it’s not reasonable to improve code instantly. I formed some rules about the refactoring timeliness. If I am itching to improve something, I look at these rules and think “Is that the moment when I need to refactor the code?” So, let’s talk about when refactoring is necessary and when it’s inappropriate.<!--more-->

*Disclaimer:* Most likely many of you will immediately say: “We have discussed it 60 times!” or “This is so obvious, why writing about it?” Probably, you’re right, but there is one moment: there is chaos all around. It looks like everything is clear, but in fact it’s not that clear at all. That is why I think it won’t be much harm to have another look at this issue. If you have no problems with refactoring you can just miss this post, everything is already ok for you.
 
### Too early refactoring
 
Do you remember when you had a permanent project specification that hasn’t been changed for months? I can hardly remember such a situation. We live in real world, requirements constantly change. And it’s not always about external requirements; it can be about your own requirements to the project. Here is the sample: let’s assume you got a mid-sized task for one or two days. Some classes have been created, but there is nothing to run yet – you create harsh architectural part. And here you notice that one of the parts is not very universal: “What if we need to do X in six months, everyone will suffer”. It’s understandable that you don’t want to commit bad code to the repository to make other developers apply harsh epithets to you. And you start refactoring the unfinished feature. Sometimes it’s reasonable, but there should be a “DANGER” label on this way. You will fix one issue, then another one, then one more issue. A week has passed, the feature still can’t run, but you say: “It’s all done inappropriately. Now I really know how I need to do it. I will re-write everything from scratch.” The main problem is that you have no feedback on the feature and you have already started improving code base. Such approach will hardly bring much success. I don’t know how it is about you, I often begin to understand that the feature should work a bit differently right after it’s been finished. And this is not because I am so stupid and couldn’t think it over properly. You need to touch some functionality to understand how it should be done in the release version. Sometimes a small prototype (allowing bad code and bugs) is necessary to discuss the feature with colleagues. Sometimes you need to show a thing to the customer to get his feedback: “No, I didn’t want it in this way, you need to do it in the opposite way”. Users don’t like innovations, they want everything as it was before. The problem with new features is that it’s difficult to predict their future. Very often all practical work goes to trash because the team decided to do it in a different way after some discussions. Summary: don’t refactor too early especially if you are not sure your code will stay in the project.
 
### Off purpose refactoring
 
Most likely you have development plan for the nearest future. Most likely there is the deadline (even if you set it). Projects should be released in time, don’t delay it. You need to control yourself, you need to do the things that are within your direct purposes. Assume, you’ve got code snippet that looks like… Looks awfully. And you don’t work with this code at the moment. This code works stably, does the job successfully and is not connected to your current task. So, don’t touch it! Yes, you can feel sad that the other part of the project isn’t that good. But notice that it doesn’t affect you in any way. You have current tasks, work on them. Of course, there are tasks to improve code base; but very often it’s usually more important to add new features or fix bugs. Focus on your current tasks and don’t delay them because something is wrong somewhere.
 
### Refactoring for refactoring
 
Ok, you came to conclusion that you certainly need to refactor some part of your project. Well, let’s refactor. It seems that all planned modifications are done and here you get an idea: “What else can be improved? Here is the thing.” And there obviously will be another thing and then another, and one more thing, etc. It’s necessary to understand that there is bad code, good code and ideal code. The last one will never be available in a big project. It doesn’t mean that you don’t need to achieve it, but you need to understand its inaccessibility. Usually the task is to write good code, not ideal. Assume that after refactoring, you got quite understandable code that works in an obvious manner, that doesn’t contain kludges and that is quite easy-to-use. Ask yourself: “May it’s time to stop?” Yes, you can continue improving the code. And you can do it infinitely in a quite big project. But right now it does the job, it’s convenient to use, it almost doesn’t annoy you. It’s very important to determine acceptable quality of code that prevents you from further improvement (until its acceptability is lost). Remember that there are so many cool things that you can create. Don’t refactor for refactoring, for ideal code. It’s necessary to refactor when you have solid reasons: the code is unreadable, it’s difficult to maintain, develop and use. If none of these reasons appear, you don’t need refactoring.
 
### A-day-before-release refactoring
 
It happens that the release should be delivered today/ tomorrow/the day after tomorrow (underline the applicable variant). This is an important moment in the project life cycle. Developers need to spend time for testing, fixing of critical bugs, finishing work. Believe me, this is a really bad idea to refactor code base (and it’s even worse – do it qualitatively) when you need to provide code to production. My experience says that it’s better to release the project and then improve code with no mess. Some developers can ask: “Why?” If there is such a question you probably have never done complicated refactoring. I will give you a hint: when you improve the code, it’s not necessarily improved – sometimes it can break. It’s not always about complicated refactoring. Sometimes you just fix a single method of five lines, miss some dependency and the other part of project gets critical bug that your users immediately face with. It seems that you don’t do anything wrong and here you are attacked by the beast called “It was obvious” and it drowns you in the pond of improper initial estimation. Though may be I am a bad developer – I like to break something. It’s possible that you always refactor in an absolutely right manner and with due control of the whole project. In this case I can congratulate you, but I won’t refuse my advice about pre-release refactoring. Believe me, refactoring won’t run away in several days and the entire team will sleep a little bit better.
 
### Refactoring of the very old code
 
The question is difficult, very difficult. The situation is as follows: there is an enormous amount of code lines that you’ve got from the previous developers (probably, this previous developer was you several years ago before you got to know how to write the correct code at once). Code should be maintained. Here and there developers add kludges and duplicates; entropy increases. Day by day you even more and more want to throw everything away and start from the very beginning. At this moment you need to think carefully about all risks. Yes, it’s possible that this activity will be helpful in the future. But in what future? and how much helpful? Most likely in the process of big refactoring or re-writing of separate parts, you will replace the old working bad code with new ideal code, yet with bugs. And this is not because you are a bad programmer and write bad code. It’s just about the fact that you may not know this code enough. You may not know why the author projected everything in this manner, and there could be some reasons. Sometimes you have to write a very rear and awkward code. I can give a lot of samples: suppression of tricky processor optimizations; adjustment to the bugs of some third party library, suppression of some multi-threaded issues, etc. I don’t say that you can’t solve these issues properly. Sometimes when you replace the absurd code with the good one, you get lots of bugs. Yes, you could do it properly, but you might not realize the entire splendor of the hut of kludges instead of sticks, if you don’t ask author of this code why it’s written in this way (and this is quite a rare opportunity). Be careful when you re-write the old code that you don’t completely understand (and especially when you think there is nothing to understand).
 

### So, when to refactor?
 
I am sorry if the first part of this article made you think that refactoring only brings problems. I still insist that the code should be readable and understandable. It also should be convenient to use and easy to maintain and develop. Positive approach is better than the negative one. So, think of refactoring not as of the source of problems, but as of your good friend who will come to rescue in an hour of need. Moreover, this friend can reduce the amount of such hours in your promising future. I would like to indicate several moments when refactoring is really relevant.
 
* **Nothing to do.** Sometimes there happens project downtime when all critical tasks are closed and the new ones haven’t been set yet. It’s not really that there is nothing to do at all, but you have some free time. Spend it to improve the code. Give understandable names to substances, get rid of duplicates, re-write inaccurate architecture. Though no new features are added, you make contribution to peace of mind of developers who will continue working in the project. Believe me, this is very important.
 
* **Everyday pain.** It happens that there is a part of the project that makes you sigh every day. And you hear similar sighs of your colleagues from the nearby working places. Of course, the release date is not tomorrow, but there are lots of important tasks. Nevertheless, week passes by week and it becomes more and more disappointing to see this code. Say: “This is enough!” If the business plan is created by your chief then you need to explain that this code just should be re-designed. If you have a customer, convince him that a week spent for refactoring will save much time in the future. If you work for users, make the decision that this time it would be better for the users to wait for a new version for one more week and then enjoy stable software and regular updates. Yes, it’s not that easy to agree with the others and with yourself, but do your best.
 
* **The problem of late refactoring.** Don’t overemphasize the rule of early refactoring. Some developers think: “Now I will improve something and it won’t come useful – that’s a shame.” You need to understand that the application engine can contain critical parts that would better be written properly. Remember that the later you make refactoring the higher will be its cost because you will spend more time and effort for re-writing. Critical base code (that is used throughout the project) should constantly be in a good condition. It would be great for your team to have the following statement work: “Refactoring never comes late. It comes exclusively when it thinks it’s necessary.”
 
* **Meeting with a monster.** You begin to add new features that should use the old project part which looks like a real monster: you’re scared only at a glance on the external interface. If you have enough time it would be better to fix code base and then focus on the new functions and don’t get distracted to add some kludges for code re-use.
 
* **Reasonable perfectionism.** Have you noticed bad code? Want to fix it? Really want to fix it? If you really want it, do it. But pay attention to the word “reasonable”. Correlate time spent for refactoring to benefits you get from code improvement. Don’t postpone the deadline and don’t go deep into infinite improving of the code. Though if you refactor timely and reasonably, the project will succeed.
 
## Summary
 
All the above is a personal summary of experience related to the work on some projects. Of course, I haven’t covered all situations. Every team has its own requirements to the code, its own business plan and its rules. I am sure many developers have a couple of stories like: “And there was a case when all the advice doesn’t work”. This is absolutely ok, it should be like this. There is no silver bullet to define the amount of effort for code optimization (like “We will refactor every day for 47 minutes and 23 seconds and everything will be ok”). In your certain team, in every certain project and based on your personal experience, you need to find the golden mean between adding the new code and improving the old code. I agitate for reasonable approach to everything, without fanaticism (“Why improve the code, no new functions will appear” / “I need to make all code ideal, so it will be fine to work with.”). Be wise when allocating time for the work with the existing code and everything will be ok for you.
 
You’re welcome with ideas and thoughts on when it’s ok or not ok to refactor. One of the most valuable things in the field is experience of real developers who work with real projects.

## Cross-posts

* [blogs.perpetuumsoft.com, Part I](http://blogs.perpetuumsoft.com/dotnet/to-refactor-or-not-to-refactor-part-i/)
* [blogs.perpetuumsoft.com, Part II](http://blogs.perpetuumsoft.com/dotnet/to-refactor-or-not-to-refactor-part-ii/)