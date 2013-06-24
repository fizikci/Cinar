﻿<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
				<div id="breadcrumbs">
					<ul class="breadcrumb">
						<li>
							<i class="icon-home"></i>
							<a href="#">Ana Sayfa</a>

							<span class="divider">
								<i class="icon-angle-right"></i>
							</span>
						</li>
						<li class="active">Dashboard</li>
					</ul><!--.breadcrumb-->

					<div id="nav-search">
						<form class="form-search">
							<span class="input-icon">
								<input type="text" placeholder="Search ..." class="input-small search-query" id="nav-search-input" autocomplete="off" />
								<i class="icon-search" id="nav-search-icon"></i>
							</span>
						</form>
					</div><!--#nav-search-->
				</div>

				<div id="page-content" class="clearfix">
					<div class="page-header position-relative">
						<h1>
							Dashboard
							<small>
								<i class="icon-double-angle-right"></i>
								overview &amp; stats
							</small>
						</h1>
					</div><!--/.page-header-->

					<div class="row-fluid">
						<!--PAGE CONTENT BEGINS HERE-->

						<div class="alert alert-block alert-success">
							<button type="button" class="close" data-dismiss="alert">
								<i class="icon-remove"></i>
							</button>

							<i class="icon-ok green"></i>

							Welcome to
							<strong class="green">
								Ace
								<small>(v1.1)</small></strong>, the lightweight, feature-rich, easy to use and well-documented admin template.
						</div>

						<div class="space-6"></div>

						<div class="row-fluid">
							<div class="span7 infobox-container">
								<div class="infobox infobox-green  ">
									<div class="infobox-icon">
										<i class="icon-comments"></i>
									</div>

									<div class="infobox-data">
										<span class="infobox-data-number">32</span>
										<div class="infobox-content">comments + 2 reviews</div>
									</div>
									<div class="stat stat-success">8%</div>
								</div>

								<div class="infobox infobox-blue  ">
									<div class="infobox-icon">
										<i class="icon-twitter"></i>
									</div>

									<div class="infobox-data">
										<span class="infobox-data-number">11</span>
										<div class="infobox-content">new followers</div>
									</div>

									<div class="badge badge-success">
										+32%
										<i class="icon-arrow-up"></i>
									</div>
								</div>

								<div class="infobox infobox-pink  ">
									<div class="infobox-icon">
										<i class="icon-shopping-cart"></i>
									</div>

									<div class="infobox-data">
										<span class="infobox-data-number">8</span>
										<div class="infobox-content">new orders</div>
									</div>
									<div class="stat stat-important">+4%</div>
								</div>

								<div class="infobox infobox-red  ">
									<div class="infobox-icon">
										<i class="icon-beaker"></i>
									</div>

									<div class="infobox-data">
										<span class="infobox-data-number">7</span>
										<div class="infobox-content">experiments</div>
									</div>
								</div>

								<div class="infobox infobox-orange2  ">
									<div class="infobox-chart">
										<span class="sparkline" data-values="196,128,202,177,154,94,100,170,224"></span>
									</div>

									<div class="infobox-data">
										<span class="infobox-data-number">6,251</span>
										<div class="infobox-content">pageviews</div>
									</div>

									<div class="badge badge-success">
										7.2%
										<i class="icon-arrow-up"></i>
									</div>
								</div>

								<div class="infobox infobox-blue2  ">
									<div class="infobox-progress">
										<div class="easy-pie-chart percentage" data-percent="42" data-size="46">
											<span class="percent">42</span>%
										</div>
									</div>

									<div class="infobox-data">
										<span class="infobox-text">traffic used</span>

										<div class="infobox-content">
											<span class="bigger-110">~</span>
											58GB remaining
										</div>
									</div>
								</div>

								<div class="space-6"></div>

								<div class="infobox infobox-green infobox-small infobox-dark">
									<div class="infobox-progress">
										<div class="easy-pie-chart percentage" data-percent="61" data-size="39">
											<span class="percent">61</span>%
										</div>
									</div>

									<div class="infobox-data">
										<div class="infobox-content">Task</div>
										<div class="infobox-content">Completion</div>
									</div>
								</div>

								<div class="infobox infobox-blue infobox-small infobox-dark">
									<div class="infobox-chart">
										<span class="sparkline" data-values="3,4,2,3,4,4,2,2"></span>
									</div>

									<div class="infobox-data">
										<div class="infobox-content">Earnings</div>
										<div class="infobox-content">$32,000</div>
									</div>
								</div>

								<div class="infobox infobox-grey infobox-small infobox-dark">
									<div class="infobox-icon">
										<i class="icon-download-alt"></i>
									</div>

									<div class="infobox-data">
										<div class="infobox-content">Downloads</div>
										<div class="infobox-content">1,205</div>
									</div>
								</div>
							</div>

							<div class="vspace"></div>

							<div class="span5">
								<div class="widget-box">
									<div class="widget-header widget-header-flat widget-header-small">
										<h5>
											<i class="icon-signal"></i>
											Traffic Sources
										</h5>

										<div class="widget-toolbar no-border">
											<button class="btn btn-minier btn-primary dropdown-toggle" data-toggle="dropdown">
												This Week
												<i class="icon-angle-down icon-on-right"></i>
											</button>

											<ul class="dropdown-menu dropdown-info pull-right dropdown-caret">
												<li class="active">
													<a href="#">This Week</a>
												</li>

												<li>
													<a href="#">Last Week</a>
												</li>

												<li>
													<a href="#">This Month</a>
												</li>

												<li>
													<a href="#">Last Month</a>
												</li>
											</ul>
										</div>
									</div>

									<div class="widget-body">
										<div class="widget-main">
											<div id="piechart-placeholder"></div>

											<div class="hr hr8 hr-double"></div>

											<div class="clearfix">
												<div class="grid3">
													<span class="grey">
														<i class="icon-facebook-sign icon-2x blue"></i>
														&nbsp; likes
													</span>
													<h4 class="bigger pull-right">1,255</h4>
												</div>

												<div class="grid3">
													<span class="grey">
														<i class="icon-twitter-sign icon-2x purple"></i>
														&nbsp; tweets
													</span>
													<h4 class="bigger pull-right">941</h4>
												</div>

												<div class="grid3">
													<span class="grey">
														<i class="icon-pinterest-sign icon-2x red"></i>
														&nbsp; pins
													</span>
													<h4 class="bigger pull-right">1,050</h4>
												</div>
											</div>
										</div><!--/widget-main-->
									</div><!--/widget-body-->
								</div><!--/widget-box-->
							</div><!--/span-->
						</div><!--/row-->

						<div class="hr hr32 hr-dotted"></div>

						<div class="row-fluid">
							<div class="span5">
								<div class="widget-box transparent">
									<div class="widget-header widget-header-flat">
										<h4 class="lighter">
											<i class="icon-star orange"></i>
											Popular Domains
										</h4>

										<div class="widget-toolbar">
											<a href="#" data-action="collapse">
												<i class="icon-chevron-up"></i>
											</a>
										</div>
									</div>

									<div class="widget-body">
										<div class="widget-main no-padding">
											<table class="table table-bordered table-striped">
												<thead>
													<tr>
														<th>
															<i class="icon-caret-right blue"></i>
															name
														</th>

														<th>
															<i class="icon-caret-right blue"></i>
															price
														</th>

														<th class="hidden-phone">
															<i class="icon-caret-right blue"></i>
															status
														</th>
													</tr>
												</thead>

												<tbody>
													<tr>
														<td>internet.com</td>

														<td>
															<small>
																<s class="red">$29.99</s>
															</small>
															<b class="green">$19.99</b>
														</td>

														<td class="hidden-phone">
															<span class="label label-info arrowed-right arrowed-in">on sale</span>
														</td>
													</tr>

													<tr>
														<td>online.com</td>

														<td>
															<small>
																<s class="red"></s>
															</small>
															<b class="green">$16.45</b>
														</td>

														<td class="hidden-phone">
															<span class="label label-success arrowed-in arrowed-in-right">approved</span>
														</td>
													</tr>

													<tr>
														<td>newnet.com</td>

														<td>
															<small>
																<s class="red"></s>
															</small>
															<b class="green">$15.00</b>
														</td>

														<td class="hidden-phone">
															<span class="label label-important arrowed">pending</span>
														</td>
													</tr>

													<tr>
														<td>web.com</td>

														<td>
															<small>
																<s class="red">$24.99</s>
															</small>
															<b class="green">$19.95</b>
														</td>

														<td class="hidden-phone">
															<span class="label arrowed">
																<s>out of stock</s>
															</span>
														</td>
													</tr>

													<tr>
														<td>domain.com</td>

														<td>
															<small>
																<s class="red"></s>
															</small>
															<b class="green">$12.00</b>
														</td>

														<td class="hidden-phone">
															<span class="label label-warning arrowed arrowed-right">SOLD</span>
														</td>
													</tr>
												</tbody>
											</table>
										</div><!--/widget-main-->
									</div><!--/widget-body-->
								</div><!--/widget-box-->
							</div>

							<div class="span7">
								<div class="widget-box transparent">
									<div class="widget-header widget-header-flat">
										<h4 class="lighter">
											<i class="icon-signal"></i>
											Sale Stats
										</h4>

										<div class="widget-toolbar">
											<a href="#" data-action="collapse">
												<i class="icon-chevron-up"></i>
											</a>
										</div>
									</div>

									<div class="widget-body">
										<div class="widget-main padding-4">
											<div id="sales-charts"></div>
										</div><!--/widget-main-->
									</div><!--/widget-body-->
								</div><!--/widget-box-->
							</div>
						</div>

						<div class="hr hr32 hr-dotted"></div>

						<div class="row-fluid">
							<div class="span6">
								<div class="widget-box transparent" id="recent-box">
									<div class="widget-header">
										<h4 class="lighter smaller">
											<i class="icon-rss orange"></i>
											RECENT
										</h4>

										<div class="widget-toolbar no-border">
											<ul class="nav nav-tabs" id="recent-tab">
												<li class="active">
													<a data-toggle="tab" href="#task-tab">Tasks</a>
												</li>

												<li>
													<a data-toggle="tab" href="#member-tab">Members</a>
												</li>

												<li>
													<a data-toggle="tab" href="#comment-tab">Comments</a>
												</li>
											</ul>
										</div>
									</div>

									<div class="widget-body">
										<div class="widget-main padding-4">
											<div class="tab-content padding-8">
												<div id="task-tab" class="tab-pane active">
													<h4 class="smaller lighter green">
														<i class="icon-list"></i>
														Sortable Lists
													</h4>

													<ul id="tasks" class="item-list">
														<li class="item-orange">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Answering customer questions</span>
															</label>

															<div class="pull-right easy-pie-chart percentage" data-size="30" data-color="#ECCB71" data-percent="42">
																<span class="percent">42</span>%
															</div>
														</li>

														<li class="item-red">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Fixing bugs</span>
															</label>

															<div class="pull-right">
																<div class="btn-group">
																	<button class="btn btn-mini btn-info">
																		<i class="icon-edit bigger-125"></i>
																	</button>

																	<button class="btn btn-mini btn-danger ">
																		<i class="icon-trash bigger-125"></i>
																	</button>

																	<button class="btn btn-mini btn-yellow">
																		<i class="icon-flag bigger-125"></i>
																	</button>
																</div>
															</div>
														</li>

														<li class="item-default">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Adding new features</span>
															</label>

															<div class="inline pull-right position-relative">
																<button class="btn btn-minier bigger btn-yellow dropdown-toggle" data-toggle="dropdown">
																	<i class="icon-angle-down icon-only bigger-120"></i>
																</button>

																<ul class="dropdown-menu dropdown-icon-only dropdown-yellow pull-right dropdown-caret dropdown-closer">
																	<li>
																		<a href="#" class="tooltip-success" data-rel="tooltip" title="Mark&nbsp;as&nbsp;done" data-placement="left">
																			<span class="green">
																				<i class="icon-ok"></i>
																			</span>
																		</a>
																	</li>

																	<li>
																		<a href="#" class="tooltip-error" data-rel="tooltip" title="Delete" data-placement="left">
																			<span class="red">
																				<i class="icon-trash"></i>
																			</span>
																		</a>
																	</li>
																</ul>
															</div>
														</li>

														<li class="item-blue">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Upgrading scripts used in template</span>
															</label>
														</li>

														<li class="item-grey">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Adding new skins</span>
															</label>
														</li>

														<li class="item-green">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Updating server software up</span>
															</label>
														</li>

														<li class="item-pink">
															<label class="inline">
																<input type="checkbox" />
																<span class="lbl"> Cleaning up</span>
															</label>
														</li>
													</ul>
												</div>

												<div id="member-tab" class="tab-pane">
													<div class="clearfix">
														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Bob Doe's avatar" src="/assets/avatars/user.jpg" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Bob Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">20 min</span>
																</div>

																<div>
																	<span class="label label-warning">pending</span>

																	<div class="inline position-relative">
																		<button class="btn btn-minier bigger btn-yellow dropdown-toggle" data-toggle="dropdown">
																			<i class="icon-angle-down icon-only bigger-120"></i>
																		</button>

																		<ul class="dropdown-menu dropdown-icon-only dropdown-yellow pull-right dropdown-caret dropdown-close">
																			<li>
																				<a href="#" class="tooltip-success" data-rel="tooltip" title="Approve" data-placement="right">
																					<span class="green">
																						<i class="icon-ok"></i>
																					</span>
																				</a>
																			</li>

																			<li>
																				<a href="#" class="tooltip-warning" data-rel="tooltip" title="Reject" data-placement="right">
																					<span class="orange">
																						<i class="icon-remove"></i>
																					</span>
																				</a>
																			</li>

																			<li>
																				<a href="#" class="tooltip-error" data-rel="tooltip" title="Delete" data-placement="right">
																					<span class="red">
																						<i class="icon-trash"></i>
																					</span>
																				</a>
																			</li>
																		</ul>
																	</div>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Joe Doe's avatar" src="/assets/avatars/avatar2.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Joe Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">1 hour</span>
																</div>

																<div>
																	<span class="label label-warning">pending</span>

																	<div class="inline position-relative">
																		<button class="btn btn-minier bigger btn-yellow dropdown-toggle" data-toggle="dropdown">
																			<i class="icon-angle-down icon-only bigger-120"></i>
																		</button>

																		<ul class="dropdown-menu dropdown-icon-only dropdown-yellow pull-right dropdown-caret dropdown-close">
																			<li>
																				<a href="#" class="tooltip-success" data-rel="tooltip" title="Approve" data-placement="right">
																					<span class="green">
																						<i class="icon-ok"></i>
																					</span>
																				</a>
																			</li>

																			<li>
																				<a href="#" class="tooltip-warning" data-rel="tooltip" title="Reject" data-placement="right">
																					<span class="orange">
																						<i class="icon-remove"></i>
																					</span>
																				</a>
																			</li>

																			<li>
																				<a href="#" class="tooltip-error" data-rel="tooltip" title="Delete" data-placement="right">
																					<span class="red">
																						<i class="icon-trash"></i>
																					</span>
																				</a>
																			</li>
																		</ul>
																	</div>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Jim Doe's avatar" src="/assets/avatars/avatar.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Jim Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">2 hour</span>
																</div>

																<div>
																	<span class="label label-warning">pending</span>

																	<div class="inline position-relative">
																		<button class="btn btn-minier bigger btn-yellow dropdown-toggle" data-toggle="dropdown">
																			<i class="icon-angle-down icon-only bigger-120"></i>
																		</button>

																		<ul class="dropdown-menu dropdown-icon-only dropdown-yellow pull-right dropdown-caret dropdown-close">
																			<li>
																				<a href="#" class="tooltip-success" data-rel="tooltip" title="Approve" data-placement="right">
																					<span class="green">
																						<i class="icon-ok"></i>
																					</span>
																				</a>
																			</li>

																			<li>
																				<a href="#" class="tooltip-warning" data-rel="tooltip" title="Reject" data-placement="right">
																					<span class="orange">
																						<i class="icon-remove"></i>
																					</span>
																				</a>
																			</li>

																			<li>
																				<a href="#" class="tooltip-error" data-rel="tooltip" title="Delete" data-placement="right">
																					<span class="red">
																						<i class="icon-trash"></i>
																					</span>
																				</a>
																			</li>
																		</ul>
																	</div>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Alex Doe's avatar" src="/assets/avatars/avatar2.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Alex Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">3 hour</span>
																</div>

																<div>
																	<span class="label label-important">blocked</span>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Bob Doe's avatar" src="/assets/avatars/avatar2.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Bob Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">6 hour</span>
																</div>

																<div>
																	<span class="label label-success arrowed-in">approved</span>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Susan's avatar" src="/assets/avatars/avatar3.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Susan</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">yesterday</span>
																</div>

																<div>
																	<span class="label label-success arrowed-in">approved</span>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Phil Doe's avatar" src="/assets/avatars/avatar4.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Phil Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">2 days ago</span>
																</div>

																<div>
																	<span class="label label-info arrowed-in  arrowed-in-right">online</span>
																</div>
															</div>
														</div>

														<div class="itemdiv memberdiv">
															<div class="user">
																<img alt="Alexa Doe's avatar" src="/assets/avatars/avatar1.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Alexa Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">3 days ago</span>
																</div>

																<div>
																	<span class="label label-success arrowed-in">approved</span>
																</div>
															</div>
														</div>
													</div>

													<div class="center">
														<i class="icon-group icon-2x green"></i>

														&nbsp;
														<a href="#">
															See all members &nbsp;
															<i class="icon-arrow-right"></i>
														</a>
													</div>

													<div class="hr hr-double hr8"></div>
												</div><!--member-tab-->

												<div id="comment-tab" class="tab-pane">
													<div class="comments">
														<div class="itemdiv commentdiv">
															<div class="user">
																<img alt="Bob Doe's Avatar" src="/assets/avatars/avatar.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Bob Doe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="green">6 min</span>
																</div>

																<div class="text">
																	<i class="icon-quote-left"></i>
																	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis &hellip;
																</div>
															</div>

															<div class="tools">
																<div class="inline position-relative">
																	<button class="btn btn-minier bigger btn-yellow dropdown-toggle" data-toggle="dropdown">
																		<i class="icon-angle-down icon-only bigger-120"></i>
																	</button>

																	<ul class="dropdown-menu dropdown-icon-only dropdown-yellow pull-right dropdown-caret dropdown-close">
																		<li>
																			<a href="#" class="tooltip-success" data-rel="tooltip" title="Approve" data-placement="left">
																				<span class="green">
																					<i class="icon-ok"></i>
																				</span>
																			</a>
																		</li>

																		<li>
																			<a href="#" class="tooltip-warning" data-rel="tooltip" title="Reject" data-placement="left">
																				<span class="orange">
																					<i class="icon-remove"></i>
																				</span>
																			</a>
																		</li>

																		<li>
																			<a href="#" class="tooltip-error" data-rel="tooltip" title="Delete" data-placement="left">
																				<span class="red">
																					<i class="icon-trash"></i>
																				</span>
																			</a>
																		</li>
																	</ul>
																</div>
															</div>
														</div>

														<div class="itemdiv commentdiv">
															<div class="user">
																<img alt="Jennifer's Avatar" src="/assets/avatars/avatar1.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Jennifer</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="blue">15 min</span>
																</div>

																<div class="text">
																	<i class="icon-quote-left"></i>
																	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis &hellip;
																</div>
															</div>

															<div class="tools">
																<a href="#" class="btn btn-minier btn-info">
																	<i class="icon-only icon-pencil"></i>
																</a>

																<a href="#" class="btn btn-minier btn-danger">
																	<i class="icon-only icon-trash"></i>
																</a>
															</div>
														</div>

														<div class="itemdiv commentdiv">
															<div class="user">
																<img alt="Joe's Avatar" src="/assets/avatars/avatar2.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Joe</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="orange">22 min</span>
																</div>

																<div class="text">
																	<i class="icon-quote-left"></i>
																	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis &hellip;
																</div>
															</div>

															<div class="tools">
																<a href="#" class="btn btn-minier btn-info">
																	<i class="icon-only icon-pencil"></i>
																</a>

																<a href="#" class="btn btn-minier btn-danger">
																	<i class="icon-only icon-trash"></i>
																</a>
															</div>
														</div>

														<div class="itemdiv commentdiv">
															<div class="user">
																<img alt="Rita's Avatar" src="/assets/avatars/avatar3.png" />
															</div>

															<div class="body">
																<div class="name">
																	<a href="#">Rita</a>
																</div>

																<div class="time">
																	<i class="icon-time"></i>
																	<span class="red">50 min</span>
																</div>

																<div class="text">
																	<i class="icon-quote-left"></i>
																	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis &hellip;
																</div>
															</div>

															<div class="tools">
																<a href="#" class="btn btn-minier btn-info">
																	<i class="icon-only icon-pencil"></i>
																</a>

																<a href="#" class="btn btn-minier btn-danger">
																	<i class="icon-only icon-trash"></i>
																</a>
															</div>
														</div>
													</div>

													<div class="hr hr8"></div>

													<div class="center">
														<i class="icon-comments-alt icon-2x green"></i>

														&nbsp;
														<a href="#">
															See all comments &nbsp;
															<i class="icon-arrow-right"></i>
														</a>
													</div>

													<div class="hr hr-double hr8"></div>
												</div>
											</div>
										</div><!--/widget-main-->
									</div><!--/widget-body-->
								</div><!--/widget-box-->
							</div><!--/span-->

							<div class="span6">
								<div class="widget-box ">
									<div class="widget-header">
										<h4 class="lighter smaller">
											<i class="icon-comment blue"></i>
											Conversation
										</h4>
									</div>

									<div class="widget-body">
										<div class="widget-main no-padding">
											<div class="dialogs">
												<div class="itemdiv dialogdiv">
													<div class="user">
														<img alt="Alexa's Avatar" src="/assets/avatars/avatar1.png" />
													</div>

													<div class="body">
														<div class="time">
															<i class="icon-time"></i>
															<span class="green">4 sec</span>
														</div>

														<div class="name">
															<a href="#">Alexa</a>
														</div>
														<div class="text">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis.</div>

														<div class="tools">
															<a href="#" class="btn btn-minier btn-info">
																<i class="icon-only icon-share-alt"></i>
															</a>
														</div>
													</div>
												</div>

												<div class="itemdiv dialogdiv">
													<div class="user">
														<img alt="John's Avatar" src="/assets/avatars/avatar.png" />
													</div>

													<div class="body">
														<div class="time">
															<i class="icon-time"></i>
															<span class="blue">38 sec</span>
														</div>

														<div class="name">
															<a href="#">John</a>
														</div>
														<div class="text">Raw denim you probably haven&#39;t heard of them jean shorts Austin.</div>

														<div class="tools">
															<a href="#" class="btn btn-minier btn-info">
																<i class="icon-only icon-share-alt"></i>
															</a>
														</div>
													</div>
												</div>

												<div class="itemdiv dialogdiv">
													<div class="user">
														<img alt="Bob's Avatar" src="/assets/avatars/user.jpg" />
													</div>

													<div class="body">
														<div class="time">
															<i class="icon-time"></i>
															<span class="orange">2 min</span>
														</div>

														<div class="name">
															<a href="#">Bob</a>
															<span class="label label-info arrowed arrowed-in-right">admin</span>
														</div>
														<div class="text">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque commodo massa sed ipsum porttitor facilisis.</div>

														<div class="tools">
															<a href="#" class="btn btn-minier btn-info">
																<i class="icon-only icon-share-alt"></i>
															</a>
														</div>
													</div>
												</div>

												<div class="itemdiv dialogdiv">
													<div class="user">
														<img alt="Jim's Avatar" src="/assets/avatars/avatar4.png" />
													</div>

													<div class="body">
														<div class="time">
															<i class="icon-time"></i>
															<span class="grey">3 min</span>
														</div>

														<div class="name">
															<a href="#">Jim</a>
														</div>
														<div class="text">Raw denim you probably haven&#39;t heard of them jean shorts Austin.</div>

														<div class="tools">
															<a href="#" class="btn btn-minier btn-info">
																<i class="icon-only icon-share-alt"></i>
															</a>
														</div>
													</div>
												</div>

												<div class="itemdiv dialogdiv">
													<div class="user">
														<img alt="Alexa's Avatar" src="/assets/avatars/avatar1.png" />
													</div>

													<div class="body">
														<div class="time">
															<i class="icon-time"></i>
															<span class="green">4 min</span>
														</div>

														<div class="name">
															<a href="#">Alexa</a>
														</div>
														<div class="text">Lorem ipsum dolor sit amet, consectetur adipiscing elit.</div>

														<div class="tools">
															<a href="#" class="btn btn-minier btn-info">
																<i class="icon-only icon-share-alt"></i>
															</a>
														</div>
													</div>
												</div>
											</div>

											<form>
												<div class="form-actions input-append">
													<input placeholder="Type your message here ..." type="text" class="width-75" name="message" />
													<button class="btn btn-small btn-info no-radius" onclick="return false;">
														<i class="icon-share-alt"></i>
														<span class="hidden-phone">Send</span>
													</button>
												</div>
											</form>
										</div><!--/widget-main-->
									</div><!--/widget-body-->
								</div><!--/widget-box-->
							</div><!--/span-->
						</div><!--/row-->

						<!--PAGE CONTENT ENDS HERE-->
					</div><!--/row-->
				</div><!--/#page-content-->

				<div id="ace-settings-container">
					<div class="btn btn-app btn-mini btn-warning" id="ace-settings-btn">
						<i class="icon-cog"></i>
					</div>

					<div id="ace-settings-box">
						<div>
							<div class="pull-left">
								<select id="skin-colorpicker" class="hidden">
									<option data-class="default" value="#438EB9">#438EB9</option>
									<option data-class="skin-1" value="#222A2D">#222A2D</option>
									<option data-class="skin-2" value="#C6487E">#C6487E</option>
									<option data-class="skin-3" value="#D0D0D0">#D0D0D0</option>
								</select>
							</div>
							<span>&nbsp; Choose Skin</span>
						</div>

						<div>
							<input type="checkbox" class="ace-checkbox-2" id="ace-settings-header" />
							<label class="lbl" for="ace-settings-header"> Fixed Header</label>
						</div>

						<div>
							<input type="checkbox" class="ace-checkbox-2" id="ace-settings-sidebar" />
							<label class="lbl" for="ace-settings-sidebar"> Fixed Sidebar</label>
						</div>
					</div>
				</div><!--/#ace-settings-container-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    		<script type="text/javascript">
    		    $(function () {

    		        $('.dialogs,.comments').slimScroll({
    		            height: '300px'
    		        });

    		        $('#tasks').sortable();
    		        $('#tasks').disableSelection();
    		        $('#tasks input:checkbox').removeAttr('checked').on('click', function () {
    		            if (this.checked) $(this).closest('li').addClass('selected');
    		            else $(this).closest('li').removeClass('selected');
    		        });

    		        var oldie = $.browser.msie && $.browser.version < 9;
    		        $('.easy-pie-chart.percentage').each(function () {
    		            var $box = $(this).closest('.infobox');
    		            var barColor = $(this).data('color') || (!$box.hasClass('infobox-dark') ? $box.css('color') : 'rgba(255,255,255,0.95)');
    		            var trackColor = barColor == 'rgba(255,255,255,0.95)' ? 'rgba(255,255,255,0.25)' : '#E2E2E2';
    		            var size = parseInt($(this).data('size')) || 50;
    		            $(this).easyPieChart({
    		                barColor: barColor,
    		                trackColor: trackColor,
    		                scaleColor: false,
    		                lineCap: 'butt',
    		                lineWidth: parseInt(size / 10),
    		                animate: oldie ? false : 1000,
    		                size: size
    		            });
    		        })

    		        $('.sparkline').each(function () {
    		            var $box = $(this).closest('.infobox');
    		            var barColor = !$box.hasClass('infobox-dark') ? $box.css('color') : '#FFF';
    		            $(this).sparkline('html', { tagValuesAttribute: 'data-values', type: 'bar', barColor: barColor, chartRangeMin: $(this).data('min') || 0 });
    		        });




    		        var data = [
                      { label: "social networks", data: 38.7, color: "#68BC31" },
                      { label: "search engines", data: 24.5, color: "#2091CF" },
                      { label: "ad campaings", data: 8.2, color: "#AF4E96" },
                      { label: "direct traffic", data: 18.6, color: "#DA5430" },
                      { label: "other", data: 10, color: "#FEE074" }
    		        ];

    		        var placeholder = $('#piechart-placeholder').css({ 'width': '90%', 'min-height': '150px' });
    		        $.plot(placeholder, data, {

    		            series: {
    		                pie: {
    		                    show: true,
    		                    tilt: 0.8,
    		                    highlight: {
    		                        opacity: 0.25
    		                    },
    		                    stroke: {
    		                        color: '#fff',
    		                        width: 2
    		                    },
    		                    startAngle: 2

    		                }
    		            },
    		            legend: {
    		                show: true,
    		                position: "ne",
    		                labelBoxBorderColor: null,
    		                margin: [-30, 15]
    		            }
                      ,
    		            grid: {
    		                hoverable: true,
    		                clickable: true
    		            },
    		            tooltip: true, //activate tooltip
    		            tooltipOpts: {
    		                content: "%s : %y.1",
    		                shifts: {
    		                    x: -30,
    		                    y: -50
    		                }
    		            }

    		        });


    		        var $tooltip = $("<div class='tooltip top in' style='display:none;'><div class='tooltip-inner'></div></div>").appendTo('body');
    		        placeholder.data('tooltip', $tooltip);
    		        var previousPoint = null;

    		        placeholder.on('plothover', function (event, pos, item) {
    		            if (item) {
    		                if (previousPoint != item.seriesIndex) {
    		                    previousPoint = item.seriesIndex;
    		                    var tip = item.series['label'] + " : " + item.series['percent'] + '%';
    		                    $(this).data('tooltip').show().children(0).text(tip);
    		                }
    		                $(this).data('tooltip').css({ top: pos.pageY + 10, left: pos.pageX + 10 });
    		            } else {
    		                $(this).data('tooltip').hide();
    		                previousPoint = null;
    		            }

    		        });






    		        var d1 = [];
    		        for (var i = 0; i < Math.PI * 2; i += 0.5) {
    		            d1.push([i, Math.sin(i)]);
    		        }

    		        var d2 = [];
    		        for (var i = 0; i < Math.PI * 2; i += 0.5) {
    		            d2.push([i, Math.cos(i)]);
    		        }

    		        var d3 = [];
    		        for (var i = 0; i < Math.PI * 2; i += 0.2) {
    		            d3.push([i, Math.tan(i)]);
    		        }


    		        var sales_charts = $('#sales-charts').css({ 'width': '100%', 'height': '220px' });
    		        $.plot("#sales-charts", [
                        { label: "Domains", data: d1 },
                        { label: "Hosting", data: d2 },
                        { label: "Services", data: d3 }
    		        ], {
    		            hoverable: true,
    		            shadowSize: 0,
    		            series: {
    		                lines: { show: true },
    		                points: { show: true }
    		            },
    		            xaxis: {
    		                tickLength: 0
    		            },
    		            yaxis: {
    		                ticks: 10,
    		                min: -2,
    		                max: 2,
    		                tickDecimals: 3
    		            },
    		            grid: {
    		                backgroundColor: { colors: ["#fff", "#fff"] },
    		                borderWidth: 1,
    		                borderColor: '#555'
    		            }
    		        });


    		        $('#recent-box [data-rel="tooltip"]').tooltip({ placement: tooltip_placement });
    		        function tooltip_placement(context, source) {
    		            var $source = $(source);
    		            var $parent = $source.closest('.tab-content')
    		            var off1 = $parent.offset();
    		            var w1 = $parent.width();

    		            var off2 = $source.offset();
    		            var w2 = $source.width();

    		            if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
    		            return 'left';
    		        }
    		    })
		</script>

</asp:Content>

